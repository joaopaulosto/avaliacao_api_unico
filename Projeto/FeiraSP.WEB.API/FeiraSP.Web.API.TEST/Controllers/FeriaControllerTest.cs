using FeiraSP.Web.API.TEST.Logger;
using FeiraSP.Web.API.TEST.ServiceFake;
using FeiraSP.Web.API.TEST.Util;
using FeiraSP.WEB.API.Controllers;
using FeiraSP.WEB.API.CustomLog;
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

        
    }
}
