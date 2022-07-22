using AulaAWS.Application.DTOs;
using AulaAWS.Lib.Models;
using Microsoft.AspNetCore.Http;

namespace AulaAWS.Application.Services
{
    public interface IUsuarioApplication
    {
         Task<int> CadastrarUsuario(UsuarioDTO dto);
         Task<List<Usuario>> ListarUsuarios();
         Task AlterarSenhaUsuario(int id, string senha);
         Task DeletarUsuario(int id);
         Task CadastrarImagemUsuario(int id, IFormFile imagem);
         Task<int> LoginUsuario(string email, string senha);
         Task<bool> LoginUsuarioImagem(int id, IFormFile imagem);
    }
}