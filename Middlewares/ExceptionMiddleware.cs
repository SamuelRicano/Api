using Microsoft.AspNetCore.Http;
using SmartClaims.Api.Exceptions;
namespace SmartClaims.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (CotizacionDuplicadaException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Se intentó crear una cotización duplicada. Número: {Numero}",
                    ex.Numero
                );


                context.Response.StatusCode = StatusCodes.Status409Conflict;

                await context.Response.WriteAsJsonAsync(new
                {
                    mensaje = ex.Message,
                    numero = ex.Numero
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    mensaje = "Ocurrió un error interno en el servidor."
                });
            }
        }
    }
}
