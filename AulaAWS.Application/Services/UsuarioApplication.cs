using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using AulaAWS.Application.DTOs;
using AulaAWS.Lib.Data.Repositorios.Interfaces;
using AulaAWS.Lib.Models;
using Microsoft.AspNetCore.Http;
using AulaAWS.Services.Services;

namespace AulaAWS.Application.Services
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IImagensServices _imagensServices;

        public UsuarioApplication(IUsuarioRepositorio repositorio, IImagensServices imagensServices)
        {
            _repositorio = repositorio;
            _imagensServices = imagensServices;
        }

        public async Task<Guid> CadastrarUsuario(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario(usuarioDto.Nome, usuarioDto.Cpf, usuarioDto.DataNascimento, usuarioDto.Email, usuarioDto.Senha);
            await _repositorio.AdicionarAsync(usuario);
            return usuario.Id;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            var listaUsuarios = await _repositorio.ListarTodosAsync();
            return listaUsuarios;
        }

        public async Task AlterarSenhaUsuario(Guid id, string senha)
        {
            await _repositorio.AlterarSenhaAsync(id, senha);
        }

        public async Task DeletarUsuario(Guid id)
        {
            await _repositorio.DeletarAsync(id);
        }

        public async Task CadastrarImagemUsuario(Guid id, IFormFile imagem)
        {
            var imagemValida = await _imagensServices.ValidarImagem(imagem);
            if (!imagemValida)
                throw new Exception("Imagem inválida");

            var nomeArquivo = await _imagensServices.SalvarNoS3(imagem);
            await _repositorio.AtualizarImagemAsync(id, nomeArquivo);
        }

        public async Task<Guid> LoginUsuario(string email, string senha)
        {
            var usuario = await _repositorio.BuscarUsuarioPorEmail(email);
            var senhaEstaCorreta = VerificarSenha(senha, usuario.Senha);
            if (!senhaEstaCorreta)
                throw new Exception("Senha incorreta");

            return usuario.Id;
        }

        public async Task<bool> LoginUsuarioImagem(Guid id, IFormFile imagem)
        {
            var usuario = await _repositorio.BuscarPorIdAsync(id);
            var imagemUsuario = await _imagensServices.BuscarImagemUsuario(usuario.UrlImagemCadastro);
            var ImagemValidada = await _imagensServices.ValidarImagemLogin(imagemUsuario, imagem);
            if (!ImagemValidada)
                throw new Exception("Foto inválida");
            return true;
        }
        private bool VerificarSenha(string senhaLogin, string senhaUsuario)
        {
            return senhaLogin == senhaUsuario;
        }
    }
}