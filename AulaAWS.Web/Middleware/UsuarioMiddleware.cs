using AulaAWS.Application.DTOs;
using Newtonsoft.Json;

namespace AulaAWS.Web.Middleware
{
    public class UsuarioMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UsuarioMiddleware> _logger;

        public UsuarioMiddleware(RequestDelegate next, 
                                 ILogger<UsuarioMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                context.Response.StatusCode = 400;
                var responseMessage = JsonConvert.SerializeObject(new ErrorResponseModel(ex.Message));
                _logger.LogError(ex.StackTrace);
                _logger.LogError(ex.Message);
                await context.Response.WriteAsJsonAsync(responseMessage);
            }
        }
    }
}