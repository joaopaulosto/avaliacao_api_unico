using FeiraSP.WEB.API.Model.DTO;
using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace FeiraSP.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeirasController : Controller
    {

        //private readonly ILogger<FeirasController> _logger;
        private readonly IFeiraService _feiraService;

        private readonly string urlConsulta = "/api/feira/{0}";

        public FeirasController(IFeiraService feiraService)
        {
            _feiraService = feiraService;
        }

        [HttpGet()]
        public IActionResult ConsultaFeira([FromQuery(Name = "Distrito")] int? distritoId, [FromQuery] string? regiao5, [FromQuery(Name = "nome_feira")] string? nome, [FromQuery] string? bairro)
        {

            var result = _feiraService.PesquisaFeira(distritoId, regiao5, nome, bairro);
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }


        [HttpGet("{id}")]
        public IActionResult ConsultaFeira([FromRoute] int id)
        {
            var result = _feiraService.BuscarPorId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CadastraFeira([FromBody] FeiraRequestDto feiraDto)
        {
            var id = _feiraService.Criar(feiraDto);
            if (id > 0)
                return Created(String.Format(urlConsulta, id), "Registro Criado com sucesso");

            return BadRequest();
        }
        [HttpPut]
        public IActionResult AtualizaFeira([FromBody] FeiraRequestDto feiraDto)
        {
            var isAtualizado = _feiraService.Atualizar(feiraDto);
            if (isAtualizado)
                return Accepted(String.Format(urlConsulta, feiraDto.Id), "Registro Atualizado com sucesso");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluiFeira([FromRoute] int id)
        {
            var isExcluido = _feiraService.Excluir(id);
            if (isExcluido)
                return Ok();

            return NoContent();
        }
    }
}
