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
    internal class SubPrefeituraServiceFake : ISubPrefeituraService
    {
        private readonly List<SubPrefeituraResponseDto> _lstSubPrefeituraResp;

        public SubPrefeituraServiceFake() => _lstSubPrefeituraResp = getDataFromJson();

        public SubPrefeituraServiceFake(List<SubPrefeituraResponseDto> list)
        {
            _lstSubPrefeituraResp = list;
        }

        public IList<SubPrefeituraResponseDto> Pesquisa()
        {
            return _lstSubPrefeituraResp;
        }

        private List<SubPrefeituraResponseDto> getDataFromJson()
        {

            List<SubPrefeituraResponseDto> items;
            using (StreamReader r = new StreamReader("SubPrefeituraForTest.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<SubPrefeituraResponseDto>>(json);
            }
            return items;

        }

    }
}
