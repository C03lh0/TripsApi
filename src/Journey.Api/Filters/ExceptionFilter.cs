using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CRUDException crudException)
            {
                context.HttpContext.Response.StatusCode = (int)crudException.GetStatusCode();
                var resposeJson = new ResponseErrosJson(crudException.GetErrorMessages());
                context.Result = new ObjectResult(resposeJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var resposeJson = new ResponseErrosJson(new List<string> { "Erro desconhecido "});
                context.Result = new ObjectResult(resposeJson);
            }
        }
    }
}
