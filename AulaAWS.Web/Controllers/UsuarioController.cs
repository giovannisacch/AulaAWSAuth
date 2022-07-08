using Microsoft.AspNetCore.Mvc;
using AulaAWS.Lib.Models;
using AulaAWS.Web.DTOs;

namespace AulaAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        public static List<Usuario> ListaUsuarios { get; set; } = new List<Usuario>();

        [HttpGet]
        public IActionResult ListaTodosUsuarios()
        {
            return Ok(ListaUsuarios);
        }
        [HttpPost]
        public IActionResult AdicionarUsuario(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario(usuarioDto.Id, usuarioDto.Nome, usuarioDto.Cpf, usuarioDto.DataNascimento, usuarioDto.Email, usuarioDto.Senha);
            ListaUsuarios.Add(usuario);
            return Ok(usuario);
        }
        [HttpPut]
        public IActionResult AlterarSenha(int id, string senha)
        {
            var usuario = ListaUsuarios.Find(x => x.Id == id);
            usuario.SetSenha(senha);
            return Ok(usuario);
        }
        [HttpDelete]
        public IActionResult DeletarUsuario(int id)
        {
            ListaUsuarios.RemoveAll(x => x.Id == id);
            return Ok("Usuario removido com sucesso!");
        }
    }
}