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
            try
            {
                var idUsuario = await _application.CadastrarUsuario(usuarioDto);
                return Ok(idUsuario);
            }
            catch (System.Exception)
            {
                return BadRequest("Deu Erro");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosUsuarios()
        {
            try
            {
                return Ok(await _application.ListarUsuarios());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarSenhaUsuario(int id, string senha)
        {
            try
            {
                await _application.AlterarSenhaUsuario(id, senha);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            try
            {
                await _application.DeletarUsuario(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("imagem")]
        public async Task<IActionResult> CadastrarUsuarioImagem(int id, IFormFile imagem)
        {
            try
            {
                await _application.CadastrarImagemUsuario(id, imagem);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginUsuario(string email, string senhaLogin)
        {
            try
            {
                return Ok(await _application.LoginUsuario(email, senhaLogin));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login/Imagem")]
        public async Task<IActionResult> LoginUsuarioImagem(int id, IFormFile imagemLogin)
        {
            try
            {
                return Ok(await _application.LoginUsuarioImagem(id, imagemLogin));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}