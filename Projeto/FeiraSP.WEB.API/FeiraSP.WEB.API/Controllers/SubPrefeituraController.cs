using FeiraSP.WEB.API.CustomLog;
using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeiraSP.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubPrefeituraController : Controller
    {
        private readonly ISubPrefeituraService _SubPrefeituraService;
        private readonly IFeiraLog _logger;


        public SubPrefeituraController(ISubPrefeituraService subPrefeituraService, IFeiraLog logger)
        {
            _SubPrefeituraService = subPrefeituraService;
            _logger = logger;
        }

        /// <summary>
        /// Consulta os dados das SubPrefeituras da cidade de São Paulo que faz parte do sistema de feira
        /// </summary>        
        /// <returns>Lista de todos as SubPrefeitura onde acontecem as Ferias de São Paulo</returns>
        /// <response code="200">Retorno quando encontrado registros</response>
        /// <response code="204">Retorno quando não encontrado registros</response>
        [HttpGet()]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Consulta()
        {

            _logger.Information("Consultando SubPrefeituras");
            var result = _SubPrefeituraService.Pesquisa();
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }
    }
}
