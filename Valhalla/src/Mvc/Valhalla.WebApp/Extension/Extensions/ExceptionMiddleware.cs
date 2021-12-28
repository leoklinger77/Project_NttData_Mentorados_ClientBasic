using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Valhalla.Dominio;
using Valhalla.Dominio.Interfaces;

namespace Valhalla.WebApp.Extension.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate nest)
        {
            _next = nest;
        }

        public async Task InvokeAsync(HttpContext httpContext, INotifierService notifierService)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DomainException e)
            {
                notifierService.AddError(e.Message);
                httpContext.Response.Redirect($"{httpContext.Request.Path}");
            }            
        }
    }
}
