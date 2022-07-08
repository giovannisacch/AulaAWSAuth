using Microsoft.AspNetCore.Mvc;
using AulaAWS.Lib.Models;
using AulaAWS.Web.DTOs;
using AulaAWS.Lib.Data.Repositorios.Interfaces;

namespace AulaAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repositorio;
        public static List<Usuario> ListaUsuarios { get; set; } = new List<Usuario>();

        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_repositorio.ListarTodos());
        }
        [HttpPost]
        public IActionResult Adicionar(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario(usuarioDto.Id, usuarioDto.Nome, usuarioDto.Cpf, usuarioDto.DataNascimento, usuarioDto.Email, usuarioDto.Senha);
            _repositorio.Adicionar(usuario);
            return Ok(usuario);
        }
        [HttpPut]
        public IActionResult Alterar(int id, string senha)
        {
            _repositorio.AlterarSenha(id, senha);
            return Ok("Senha alterada com sucesso!");
        }
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            _repositorio.Deletar(id);
            return Ok("Usuario removido com sucesso!");
        }
    }
}