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
        public async Task<IActionResult> ListarTodos()
        {
            return Ok(await _repositorio.ListarTodosAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Adicionar(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario(usuarioDto.Id, usuarioDto.Nome, usuarioDto.Cpf, usuarioDto.DataNascimento, usuarioDto.Email, usuarioDto.Senha);
            await _repositorio.AdicionarAsync(usuario);
            return Ok(usuario);
        }
        [HttpPut]
        public async Task<IActionResult> Alterar(int id, string senha)
        {
            await _repositorio.AlterarSenhaAsync(id, senha);
            return Ok("Senha alterada com sucesso!");
        }
        [HttpDelete]
        public async Task<IActionResult> Deletar(int id)
        {
            await _repositorio.DeletarAsync(id);
            return Ok("Usuario removido com sucesso!");
        }
    }
}