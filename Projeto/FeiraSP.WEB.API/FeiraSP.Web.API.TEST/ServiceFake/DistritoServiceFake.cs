using FeiraSP.WEB.API.Model.DTO;
using FeiraSP.WEB.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraSP.Web.API.TEST.ServiceFake
{
    internal class DistritoServiceFake: IDistritoService
    {
        private readonly List<DistritoResponseDto> _lstDistritoResp;

        public DistritoServiceFake() => _lstDistritoResp = getDataFromJson();

        public DistritoServiceFake(List<DistritoResponseDto> list)
        {
            _lstDistritoResp = list;
        }

        public IList<DistritoResponseDto> Pesquisa()
        {
            return _lstDistritoResp;
        }

        private List<DistritoResponseDto> getDataFromJson()
        {

            List<DistritoResponseDto> items;
            using (StreamReader r = new StreamReader("DistritoForTests.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<DistritoResponseDto>>(json);
            }
            return items;

        }

    }
}
