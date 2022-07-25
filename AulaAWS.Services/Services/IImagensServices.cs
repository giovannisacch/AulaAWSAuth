using Amazon.Rekognition.Model;
using Microsoft.AspNetCore.Http;

namespace AulaAWS.Services.Services
{
    public interface IImagensServices
    {
         Task<string> SalvarNoS3(IFormFile imagem);
         Task<bool> ValidarImagem(IFormFile arquivo);
         Task<Image> BuscarImagemUsuario(string nomeArquivo);
         Task<bool> ValidarImagemLogin(Image imagemUsuario, IFormFile imagemLogin);
    }
}