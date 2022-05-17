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
        /// <summary>
        /// Consulta os registro das ferias com base em um dos parametros abaixo
        /// </summary>
        /// <param name="distritoId">Código do Distrito</param>
        /// <param name="regiao5">Nome da Região 5</param>
        /// <param name="nome">Nome da Feira</param>
        /// <param name="bairro">Nome do Bairro onde a feira aconttece</param>
        /// <returns>Retorna uma lista de objetos que representa as feiras da cidade de São Paulo</returns>
        /// <response code="200">Retorno quando encontrado registros</response>
        /// <response code="204">Retorno quando não encontrado registros</response>
        /// <response code="400">Retorno quando não informando ao menos um parâmetro</response>
        [HttpGet()]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ConsultaFeira([FromQuery(Name = "Distrito")] int? distritoId, [FromQuery] string? regiao5, [FromQuery(Name = "nome_feira")] string? nome, [FromQuery] string? bairro)
        {

            _logger.Information(String.Format("Consultando Feira pelos campos: Distrito {0} / Regiao5  {1} / Nome {2} / Bairro {3}", distritoId, regiao5, nome, bairro));
            var result = _feiraService.PesquisaFeira(distritoId, regiao5, nome, bairro);
            if (result.Count > 0)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Consulta um registro da feira de São Paulo através do seu Identificador
        /// </summary>
        /// <param name="id">Código da Feira</param>
        /// <returns>Retorna um objeto que representa a feira de São Paulo</returns>        
        /// <response code="200">Retorno quando encontrado registros</response>
        /// <response code="204">Retorno quando não encontrado registros</response>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]       
        public IActionResult ConsultaFeira([FromRoute] int id)
        {

            _logger.Information(String.Format("Consultando Feira pelo id: {0}", id));
            var result = _feiraService.BuscarPorId(id);
            return Ok(result);
        }
        /// <summary>
        /// Cria um registro da Feira de São Paulo
        /// </summary>
        /// <param name="feiraDto">Objeto que representa a feira de São Paulo</param>
        /// <returns>Retorna a URI de Localização do Objeto recem criado</returns>        
        /// <response code="201">Retorno quando o objeto foi criado com sucesso</response>
        /// <response code="400">Retorno quando os dados estão inconsistente</response>
        [HttpPost]        
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Atualiza um determinada Feira dado suas informações
        /// </summary>        
        /// <param name="feiraDto">Objeto que representa a feira de São Paulo</param>
        /// <returns>Retorna a URI de Localização do Objeto recem alterado</returns>        
        /// <response code="201">Retorno quando o objeto foi criado com sucesso</response>
        /// <response code="204">Retorno quando não foi localizado nenhuma alteração</response>
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
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
        /// <summary>
        /// Remove um registro com base no Código informado
        /// </summary>
        /// <param name="id">Código da Feira</param>
        /// <returns>Se o regitro foi excluido ou não com sucesso</returns>
        /// <response code="200">Retorno quando o objeto foi excluido com sucesso</response>
        /// <response code="204">Retorno quando não foi localizado para a exclusão</response>
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
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
