using System.Net;
using System.Text.Json;
using FeiraSP.WEB.API.CustomErrors;
using FeiraSP.WEB.API.CustomLog;

namespace FeiraSP.WEB.API.CustomErrors
{
    public class FeiraErrorHandler
    {

        private readonly RequestDelegate _next;
        private readonly IFeiraLog _logger;

        public FeiraErrorHandler(RequestDelegate next, IFeiraLog feiraLog)
        {
            _next = next;
            _logger = feiraLog;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception error)
            {

              
                _logger.Error(String.Format("Erro na chamada da URL {0} com os Metodo {1} e queryString: {2} Body {3}",
                    context.Request.Path, 
                    context.Request.Method, 
                    context.Request.QueryString,
                    ""));

                _logger.Error(error.Message);
                if(error.InnerException != null)
                    _logger.Error(error.InnerException.ToString());

                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case FeiraException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
