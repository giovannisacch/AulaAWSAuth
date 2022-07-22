using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using AulaAWS.Application.DTOs;
using AulaAWS.Lib.Data.Repositorios.Interfaces;
using AulaAWS.Lib.Models;
using Microsoft.AspNetCore.Http;

namespace AulaAWS.Application.Services
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IAmazonS3 _amazonS3;
        private readonly AmazonRekognitionClient _rekognitionClient;
        private readonly List<string> _imageFormats = new List<string>() { "image/jpeg", "image/png" };

        public UsuarioApplication(IUsuarioRepositorio repositorio, IAmazonS3 amazonS3, AmazonRekognitionClient rekognitionClient)
        {
            _repositorio = repositorio;
            _amazonS3 = amazonS3;
            _rekognitionClient=rekognitionClient;

        }

        public async Task<int> CadastrarUsuario(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario(usuarioDto.Id, usuarioDto.Nome, usuarioDto.Cpf, usuarioDto.DataNascimento, usuarioDto.Email, usuarioDto.Senha);
            await _repositorio.AdicionarAsync(usuario);
            return usuario.Id;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            var listaUsuarios = await _repositorio.ListarTodosAsync();
            return listaUsuarios;
        }

        public async Task AlterarSenhaUsuario(int id, string senha)
        {
            await _repositorio.AlterarSenhaAsync(id, senha);
        }

        public async Task DeletarUsuario(int id)
        {
            await _repositorio.DeletarAsync(id);
        }

        public async Task CadastrarImagemUsuario(int id, IFormFile imagem)
        {
            var imagemValida = await ValidarImagem(imagem);
            if (!imagemValida)
                throw new Exception("Imagem inválida");

            var nomeArquivo = await SalvarNoS3(imagem);
            await _repositorio.AtualizarImagemAsync(id, nomeArquivo);
        }

        public async Task<int> LoginUsuario(string email, string senha)
        {
            var usuario = await _repositorio.BuscarUsuarioPorEmail(email);
            var senhaEstaCorreta = VerificarSenha(senha, usuario.Senha);
            if (!senhaEstaCorreta)
                throw new Exception("Senha incorreta");
            
            return usuario.Id;
        }

        public async Task<bool> LoginUsuarioImagem(int id, IFormFile imagem)
        {
            var usuario = await _repositorio.BuscarPorIdAsync(id);
            var imagemUsuario = await SalvarImagemUsuario(usuario.UrlImagemCadastro);
            var ImagemValidada = await ValidarImagemLogin(imagemUsuario, imagem);
            if (!ImagemValidada)
                throw new Exception("Foto inválida");
            return true;
        }

        private async Task<string> SalvarNoS3(IFormFile imagem)
        {
            if (!_imageFormats.Contains(imagem.ContentType))
                throw new Exception("Tipo inválido da imagem");
            using (var imagemStream = new MemoryStream())
            {
                await imagem.CopyToAsync(imagemStream);

                var request = new PutObjectRequest();
                request.Key = "reconhecimento" + imagem.FileName;
                request.BucketName = "aula-imagens";
                request.InputStream = imagemStream;

                await _amazonS3.PutObjectAsync(request);
                return request.Key;
            }
        }
        private async Task<bool> ValidarImagem(IFormFile arquivo)
        {
            using (var memoryStream = new MemoryStream())
            {
                await arquivo.CopyToAsync(memoryStream);

                var request = new DetectFacesRequest();
                var imagem = new Image()
                {
                    Bytes = memoryStream
                };

                request.Image = imagem;
                request.Attributes = new List<string>() { "ALL" };

                var response = await _rekognitionClient.DetectFacesAsync(request);

                if ((response.FaceDetails.Count == 1) && (response.FaceDetails.First().Eyeglasses.Value == false))
                    return true;
                else
                    throw new Exception("Essa imagem nao contem um rosto!");
            }
        }
        private bool VerificarSenha(string senhaLogin, string senhaUsuario)
        {
            return senhaLogin == senhaUsuario;
        }
        private async Task<Image> SalvarImagemUsuario(string nomeArquivo)
        {
            var image = new Image();
            image.S3Object = new Amazon.Rekognition.Model.S3Object()
            {
                Bucket = "aula-imagens",
                Name = nomeArquivo
            };
            return image;
        }
        private async Task<bool> ValidarImagemLogin(Image imagemUsuario, IFormFile imagemLogin)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imagemLogin.CopyToAsync(memoryStream);
                var targetImage = new Image();
                targetImage.Bytes = memoryStream;

                var request = new CompareFacesRequest();
                var sourceImage = imagemUsuario;

                request.TargetImage = targetImage;
                request.SourceImage = sourceImage;

                var imagemValida = await ValidarImagem(imagemLogin);
                var response = await _rekognitionClient.CompareFacesAsync(request);
                if (imagemValida && response.FaceMatches.Count() == 1 && response.FaceMatches.First().Face.Confidence > 90)
                    return true;
                else
                    return false;
            }
        }
    }
}