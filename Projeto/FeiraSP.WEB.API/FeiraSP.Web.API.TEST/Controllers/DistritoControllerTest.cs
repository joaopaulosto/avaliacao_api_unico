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
    public class DistritoControllerTest
    {

        private readonly DistritoController _controller;
        private readonly DistritoController _controllerSemDados;
        private readonly IDistritoService _serviceDistrito;
        private readonly IDistritoService _serviceDistritoSemDados;
        private readonly IFeiraLog _serviceLog;

        public DistritoControllerTest()
        {
            _serviceDistrito = new DistritoServiceFake();
            _serviceLog = new FeiraNLog(configuraMockNLog());
            _controller = new DistritoController(_serviceDistrito, _serviceLog);

            _serviceDistritoSemDados = new DistritoServiceFake(new List<DistritoResponseDto>());
            _controllerSemDados  = new DistritoController(_serviceDistritoSemDados, _serviceLog);
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
            var retornoConsulta = _controller.ConsultaDistrito();
            Assert.IsType<OkObjectResult>(retornoConsulta);
        }



        [Fact]
        public void Get_ConsultaDistrito_RetornoVazio()
        {

            var retornoConsulta = _controllerSemDados.ConsultaDistrito();
            Assert.IsType<NoContentResult>(retornoConsulta);
        }



    }
}
