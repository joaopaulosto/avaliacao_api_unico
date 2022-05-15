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


        [HttpGet()]
        public IActionResult ConsultaDistrito()
        {

            _logger.Information("Consultando SubPrefeituras");
            var result = _SubPrefeituraService.Pesquisa();
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }
    }
}
