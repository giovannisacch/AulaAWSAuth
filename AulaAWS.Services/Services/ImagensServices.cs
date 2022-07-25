using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace AulaAWS.Services.Services
{
    public class ImagensServices : IImagensServices
    {
        private readonly List<string> _imageFormats = new List<string>() { "image/jpeg", "image/png" };
        private readonly IAmazonS3 _amazonS3;
        private readonly AmazonRekognitionClient _rekognitionClient;

        public ImagensServices(IAmazonS3 amazonS3, AmazonRekognitionClient rekognitionClient)
        {
            _amazonS3 = amazonS3;
            _rekognitionClient = rekognitionClient;
        }
        public async Task<string> SalvarNoS3(IFormFile imagem)
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
        public async Task<bool> ValidarImagem(IFormFile arquivo)
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
                    throw new Exception("Imagem inválida! Atenção a imagem deve conter somente um rosto e não pode estar usando óculos");
            }
        }
        public async Task<Image> BuscarImagemUsuario(string nomeArquivo)
        {
            var image = new Image();
            image.S3Object = new Amazon.Rekognition.Model.S3Object()
            {
                Bucket = "aula-imagens",
                Name = nomeArquivo
            };
            return image;
        }
        public async Task<bool> ValidarImagemLogin(Image imagemUsuario, IFormFile imagemLogin)
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