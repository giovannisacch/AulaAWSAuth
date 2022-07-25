using AulaAWS.Application.DTOs;
using Newtonsoft.Json;

namespace AulaAWS.Web.Middleware
{
    public class UsuarioMiddleware
    {
        private readonly RequestDelegate _next;

        public UsuarioMiddleware(RequestDelegate next)
        {
            _next = next;
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
                await context.Response.WriteAsJsonAsync(responseMessage);
            }
        }
    }
}