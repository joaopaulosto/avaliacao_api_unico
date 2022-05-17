using FeiraSP.WEB.API.CustomLog;
using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeiraSP.WEB.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DistritoController : Controller
    {
        private readonly IDistritoService _DistritoService;

        private readonly IFeiraLog _logger;

        public DistritoController(IDistritoService distritoService, IFeiraLog logger)
        {
            _DistritoService = distritoService;
            _logger = logger;
        }

        /// <summary>
        /// Consulta dos os Distritos da cidade de São Paulo que faz parte do sistema de feira
        /// </summary>
        /// <returns>Lista de todos os Distritos onde acontecem as Ferias de São Paulo</returns>
        /// <response code="200">Retorno quando encontrado registros</response>
        /// <response code="204">Retorno quando não encontrado registros</response>
        [HttpGet()]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult ConsultaDistrito()
        {

            _logger.Information("Consultando Distritos");
            var result = _DistritoService.Pesquisa();
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }
    }
}
