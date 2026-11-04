using System.Net;

namespace TheLibrary_PresentationLayer.Middleware
{
    public class ExeptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExeptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // 1. Intercepción de la solicitud (REQUEST)
                await _next(context); // 1. Intercepción
                // 2. Intercepción de la respuesta (RESPONSE)
            }
            catch (Exception ex)
            {
                
                await HandleExeptionsAsync(context, ex); // 3. Manejo
            }
        }

        private static Task HandleExeptionsAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new { statusCode = context.Response.StatusCode, message = ex.Message };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}