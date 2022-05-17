using FeiraSP.Web.API.TEST.ServiceFake;
using FeiraSP.Web.API.TEST.Util;
using FeiraSP.WEB.API.Controllers;
using FeiraSP.WEB.API.CustomLog;
using FeiraSP.WEB.API.Model.DTO;
using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraSP.Web.API.TEST.Controllers
{
    public class FeriaControllerTest
    {
        private readonly FeirasController _controller;
        private readonly IFeiraService _serviceFeira;
        private readonly IFeiraLog _serviceLog;

        public FeriaControllerTest()
        {
            _serviceFeira = new FeiraServiceFake();
            _serviceLog = new FeiraNLog(configuraMockNLog());
            _controller = new FeirasController(_serviceFeira, _serviceLog);
        }

        private Logger configuraMockNLog()
        {
            var target = new MemoryTarget { Layout = "${message}" };
            var config = new LoggingConfiguration();
            config.AddRuleForAllLevels(target);
            LogManager.Configuration = config;
            return LogManager.GetCurrentClassLogger();
        }

        [Fact]
        public void Get_Id_Desconhecido_ThrowKeyNotFound()
        {
            int id = FeiraUtil.CriarID();

            Assert.Throws<KeyNotFoundException>(() => _controller.ConsultaFeira(id));

        }

        [Fact]
        public void Get_Id_Existente_ReturnsOKtResult()
        {

            var list = _serviceFeira.PesquisaFeira(null, null, "a", null);
            int index = FeiraUtil.CriarID(0, list.Count - 1);

            int id = list[index].Id;

            var retornoConsulta = _controller.ConsultaFeira(id);

            Assert.IsType<OkObjectResult>(retornoConsulta);

        }


        [Fact]
        public void Get_ConsultaSemParametro_ReturnsBadRequest()
        {

            Assert.Throws<Exception>(() => _serviceFeira.PesquisaFeira(null, null, null, null));

        }

        [Fact]
        public void Post_NovaFeira_ReturnsBadRequest()
        {

            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];
            var itemFeiraReq = FeiraUtil.convertRespToReq(itemFeira);


            itemFeiraReq.Nome = string.Empty;

            _controller.ModelState.AddModelError("Nome", "Required");

            var badRequestRetorno = _controller.CadastraFeira(itemFeiraReq);

            Assert.IsType<BadRequestObjectResult>(badRequestRetorno);

        }

        [Fact]
        public void Post_NovaFeira_ReturnsSucesso()
        {

            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];
            var itemFeiraReq = FeiraUtil.convertRespToReq(itemFeira);


            itemFeiraReq.Id = 0;
            itemFeira.Nome = DateTime.Now.ToString("ddMMyyy hhmmss").ToString();


            var retornoOK = _controller.CadastraFeira(itemFeiraReq);

            Assert.IsType<CreatedAtActionResult>(retornoOK);

        }

        [Fact]
        public void Post_NovaFeira_ReturnsSucesso_ValidaObjetoRetornado()
        {

            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];
            var itemFeiraReq = FeiraUtil.convertRespToReq(itemFeira);


            itemFeiraReq.Id = 0;
            string nomeDinamico = DateTime.Now.ToString("ddMMyyy hhmmss").ToString();
            itemFeiraReq.Nome = nomeDinamico;


            var retornoOK = _controller.CadastraFeira(itemFeiraReq) as CreatedAtActionResult;
            var itemCriado = retornoOK.Value as FeiraResponseDto;


            Assert.IsType<FeiraResponseDto>(itemCriado);
            Assert.Equal(nomeDinamico, itemCriado.Nome);

        }

        [Fact]
        public void Delete_Feira_CodigoInexistente()
        {

            int index = FeiraUtil.CriarID(5000, 6000);

            var retornoExclusao = _controller.ExcluiFeira(index);

            Assert.IsType<NoContentResult>(retornoExclusao);

        }


        [Fact]
        public void Delete_Feira_CodigoExistente()
        {
            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];

            var retornoExclusao = _controller.ExcluiFeira(itemFeira.Id);

            Assert.IsType<OkResult>(retornoExclusao);

        }


        [Fact]
        public void Delete_Feira_VerificaPosExclusao()
        {
            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];

            int idFeira = itemFeira.Id;

            var retornoExclusao = _controller.ExcluiFeira(idFeira);

            Assert.IsType<OkResult>(retornoExclusao);

            Assert.Throws<KeyNotFoundException>(() => _controller.ConsultaFeira(idFeira));

        }


        [Fact]
        public void Put_AlteraFeira_ObjetoNull()
        {

            FeiraRequestDto feiraDto = null;
            Assert.Throws<NullReferenceException>(() => _controller.AtualizaFeira(feiraDto));

        }

        [Fact]
        public void Put_AlteraFeira_CodigoIndexistente()
        {
            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];

            var feiraReq = FeiraUtil.convertRespToReq(itemFeira);

            feiraReq.Id = FeiraUtil.CriarID(5000, 5100);
            
            Assert.Throws<KeyNotFoundException>(() => _controller.AtualizaFeira(feiraReq));

        }

        [Fact]
        public void Put_AlteraFeira_SemAlteracao()
        {
            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];

            var feiraReq = FeiraUtil.convertRespToReq(itemFeira);

            var retornoAtualizacao = _controller.AtualizaFeira(feiraReq);

            Assert.IsType<NoContentResult>(retornoAtualizacao);            

        }

        [Fact]
        public void Put_AlteraFeira_Sucesso()
        {
            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];

            var feiraReq = FeiraUtil.convertRespToReq(itemFeira);
            feiraReq.Nome = DateTime.Now.ToString("ddMMyyy hhmmss").ToString();

            var retornoAtualizacao = _controller.AtualizaFeira(feiraReq);

            Assert.IsType<AcceptedResult>(retornoAtualizacao);

        }


        [Fact]
        public void Put_AlteraFeira_SucessoValidaAlteracao()
        {
            var itemFeira = _serviceFeira.PesquisaFeira(null, null, "a", null)[0];

            var feiraReq = FeiraUtil.convertRespToReq(itemFeira);
            string nomeAntesAlteracao = feiraReq.Nome;
            feiraReq.Nome = DateTime.Now.ToString("ddMMyyy hhmmss").ToString();

            var retornoAtualizacao = _controller.AtualizaFeira(feiraReq);

            Assert.IsType<AcceptedResult>(retornoAtualizacao);

            var retornoConsulta = _controller.ConsultaFeira(feiraReq.Id) as OkObjectResult ;
            var feiraConsulta = retornoConsulta.Value as FeiraResponseDto;

            Assert.NotEqual(feiraConsulta.Nome, nomeAntesAlteracao);



        }


    }
}
