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


        [HttpGet()]
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
