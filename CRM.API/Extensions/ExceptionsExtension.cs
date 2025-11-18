using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using System.Text;

namespace CRM.API.Extensions
{
    public static class ExceptionsExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = (c) =>
                {
                    var exception = c.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exception.Error.GetType().Name switch
                    {
                        "ArgumentException" => HttpStatusCode.BadRequest,
                        "ArgumentNullException" => HttpStatusCode.InternalServerError,
                        "Win32Exception" => HttpStatusCode.InternalServerError,
                        _ => HttpStatusCode.BadRequest
                    };


                    c.Response.StatusCode = (int)statusCode;
                    string mensagem = exception.Error.Message;

                    c.Response.ContentType = "application/json";

                    if (mensagem == "Conflict")
                        mensagem = "Operação cancelada, usuário ou e-mail já existe.";

                    Log.Debug("Erro: " + exception.Error.Message + " - " + exception.Error.StackTrace);

                    var content = Encoding.UTF8.GetBytes($"{mensagem}");
                    return c.Response.Body.WriteAsync(content, 0, content.Length);
                    //return Task.CompletedTask;
                }
            });
        }
    }
}
