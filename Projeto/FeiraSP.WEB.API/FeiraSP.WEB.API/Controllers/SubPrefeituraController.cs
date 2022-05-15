using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeiraSP.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubPrefeituraController : Controller
    { 
        private readonly ISubPrefeituraService _SubPrefeituraService;


        public SubPrefeituraController(ISubPrefeituraService subPrefeituraService) => _SubPrefeituraService = subPrefeituraService;

        [HttpGet()]
        public IActionResult ConsultaDistrito()
        {

            var result = _SubPrefeituraService.Pesquisa();
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }
    }
}
