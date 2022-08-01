using AulaAWS.Application.DTOs;
using AulaAWS.Lib.Models;
using Microsoft.AspNetCore.Http;

namespace AulaAWS.Application.Services
{
    public interface IUsuarioApplication
    {
         Task<Guid> CadastrarUsuario(UsuarioDTO dto);
         Task<List<Usuario>> ListarUsuarios();
         Task AlterarSenhaUsuario(Guid id, string senha);
         Task DeletarUsuario(Guid id);
         Task CadastrarImagemUsuario(Guid id, IFormFile imagem);
         Task<Guid> LoginUsuario(string email, string senha);
         Task<bool> LoginUsuarioImagem(Guid id, IFormFile imagem);
    }
}