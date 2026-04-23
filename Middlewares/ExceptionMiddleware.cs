using System.Net;
using good_hamburguer_system.Domain.Exceptions;

namespace good_hamburguer_system.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                await context.Response.WriteAsync(new
                {
                    error = ex.Message,
                    statusCode = ex.StatusCode
                }.ToString());
            }
            catch (Exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                await context.Response.WriteAsync(new
                {
                    error = "Erro interno inesperado"
                }.ToString());
            }
        }
    }
}