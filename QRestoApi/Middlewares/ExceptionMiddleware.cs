using System.Net;
using System.Text.Json;

namespace QRestoApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // Növbəti middleware-i çağırmaq üçün

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Hər bir HTTP sorğusu bu metoddan keçir
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Sorğu yoluna davam edir (Controller-ə gedir)
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Əgər yolda (Controller, Service, Repo) xəta çıxsa, bura düşürük
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 xətası

            // Müştəriyə (Front-end-ə) qaytaracağımız standart model
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sistemdə daxili xəta baş verdi. Zəhmət olmasa bir az sonra yenidən yoxlayın.",
                Detailed = exception.Message // Debug vaxtı xətanın nə olduğunu görmək üçün
            };

            // Obyekti JSON formatına salırıq
            var json = JsonSerializer.Serialize(response);

            // Cavabı geri göndəririk
            await context.Response.WriteAsync(json);
        }
    }
}
