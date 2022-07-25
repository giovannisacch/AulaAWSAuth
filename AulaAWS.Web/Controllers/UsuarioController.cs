using Microsoft.AspNetCore.Mvc;
using AulaAWS.Lib.Models;
using AulaAWS.Application.DTOs;
using AulaAWS.Lib.Data.Repositorios.Interfaces;
using Amazon.S3.Model;
using Amazon.S3;
using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using AulaAWS.Application.Services;

namespace AulaAWS.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication _application;
       
        public UsuarioController(IUsuarioApplication application)
        {
            _application = application;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(UsuarioDTO usuarioDto)
        {
                var idUsuario = await _application.CadastrarUsuario(usuarioDto);
                return Ok(idUsuario);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosUsuarios()
        {
                return Ok(await _application.ListarUsuarios());    
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarSenhaUsuario(int id, string senha)
        {
                await _application.AlterarSenhaUsuario(id, senha);
                return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
                await _application.DeletarUsuario(id);
                return Ok();
        }

        [HttpPost("imagem")]
        public async Task<IActionResult> CadastrarUsuarioImagem(int id, IFormFile imagem)
        {
                await _application.CadastrarImagemUsuario(id, imagem);
                return Ok();
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginUsuario(string email, string senhaLogin)
        {
                return Ok(await _application.LoginUsuario(email, senhaLogin));
        }

        [HttpPost("Login/Imagem")]
        public async Task<IActionResult> LoginUsuarioImagem(int id, IFormFile imagemLogin)
        {
                return Ok(await _application.LoginUsuarioImagem(id, imagemLogin));
        }
    }
}