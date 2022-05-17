using FeiraSP.Web.API.TEST.ServiceFake;
using FeiraSP.WEB.API.Controllers;
using FeiraSP.WEB.API.CustomLog;
using FeiraSP.WEB.API.Model.DTO;
using FeiraSP.WEB.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class SubPrefeituraControllerTest
    {

        private readonly SubPrefeituraController _controller;
        private readonly SubPrefeituraController _controllerSemDados;
        private readonly ISubPrefeituraService _service;
        private readonly ISubPrefeituraService _serviceSemDados;
        private readonly IFeiraLog _serviceLog;

        public SubPrefeituraControllerTest()
        {
            _service = new SubPrefeituraServiceFake();
            _serviceLog = new FeiraNLog(configuraMockNLog());
            _controller = new SubPrefeituraController(_service, _serviceLog);

            _serviceSemDados = new SubPrefeituraServiceFake(new List<SubPrefeituraResponseDto>());
            _controllerSemDados  = new SubPrefeituraController(_serviceSemDados, _serviceLog);
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
        public void Get_ConsultaDistrito_RetornoSucesso()
        {
            var retornoConsulta = _controller.Consulta();
            Assert.IsType<OkObjectResult>(retornoConsulta);
        }



        [Fact]
        public void Get_ConsultaDistrito_RetornoVazio()
        {

            var retornoConsulta = _controllerSemDados.Consulta();
            Assert.IsType<NoContentResult>(retornoConsulta);
        }



    }
}
