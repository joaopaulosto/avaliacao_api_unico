using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeiraSP.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistritoController : Controller
    { 
        private readonly IDistritoService _DistritoService;


        public DistritoController(IDistritoService distritoService) => _DistritoService = distritoService;

        [HttpGet()]
        public IActionResult ConsultaDistrito()
        {

            var result = _DistritoService.Pesquisa();
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }
    }
}
