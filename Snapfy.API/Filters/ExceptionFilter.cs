using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Shoalace.Domain.Interfaces.Repositories;
using System.Net;
using System.Threading.Tasks;

namespace Shoalace.API.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IErroRepository _erroRepositorio;

        public ExceptionFilter(IErroRepository erroRepositorio)
        {
            _erroRepositorio = erroRepositorio;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            long erroId = await _erroRepositorio.TratamentoException(context.Exception, $"Rota: {JsonConvert.SerializeObject(context.RouteData.Values)} - QueryString: {context.HttpContext.Request.QueryString}");
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            await response.WriteAsync($"Aconteceu um erro inesperado, por favor contate o suporte e informe este código: {erroId}");
        }
    }
}
