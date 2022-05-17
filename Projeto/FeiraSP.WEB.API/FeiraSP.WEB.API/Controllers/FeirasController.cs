using FeiraSP.WEB.API.CustomLog;
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
        
        private readonly IFeiraService _feiraService;
        private readonly IFeiraLog _logger;

        private readonly string urlConsulta = "/api/feira/{0}";

        public FeirasController(IFeiraService feiraService, IFeiraLog logger)
        {
            _feiraService = feiraService;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult ConsultaFeira([FromQuery(Name = "Distrito")] int? distritoId, [FromQuery] string? regiao5, [FromQuery(Name = "nome_feira")] string? nome, [FromQuery] string? bairro)
        {

            _logger.Information(String.Format("Consultando Feira pelos campos: Distrito {0} / Regiao5  {1} / Nome {2} / Bairro {3}", distritoId, regiao5, nome, bairro));
            var result = _feiraService.PesquisaFeira(distritoId, regiao5, nome, bairro);
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }


        [HttpGet("{id}")]
        public IActionResult ConsultaFeira([FromRoute] int id)
        {

            _logger.Information(String.Format("Consultando Feira pelo id: {0}", id));
            var result = _feiraService.BuscarPorId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CadastraFeira([FromBody] FeiraRequestDto feiraDto)
        {
            _logger.Information(String.Format("Criando Nova Feira:{0}", feiraDto));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var feiraCriada = _feiraService.Criar(feiraDto);
            if (feiraCriada.Id > 0) {
                _logger.Information(String.Format("Feira Criada com ID:{0}", feiraCriada.Id));                
                return CreatedAtAction(nameof(FeirasController.ConsultaFeira), new { id = feiraCriada.Id }, feiraCriada);
            }

            return BadRequest();
        }
        [HttpPut]
        public IActionResult AtualizaFeira([FromBody] FeiraRequestDto feiraDto)
        {
            _logger.Information(String.Format("Atualizando Feira :{0}", feiraDto));
            var isAtualizado = _feiraService.Atualizar(feiraDto);
            if (isAtualizado)
            {
                _logger.Information("Feira atualizada com sucesso");
                return Accepted(String.Format(urlConsulta, feiraDto.Id), "Registro Atualizado com sucesso");

            }
            _logger.Information("Nenhuma infromacao da feira foi atualizada");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluiFeira([FromRoute] int id)
        {
            _logger.Information(String.Format("Excluindo Feira :{0}", id));
            var isExcluido = _feiraService.Excluir(id);
            if (isExcluido)
            {
                _logger.Information(String.Format("Feira Excluindo com sucesso", id));
                return Ok();

            }
            _logger.Information(String.Format("Feira com id {0} nao encontrada para exclusao", id));
            return NoContent();
        }
    }
}
