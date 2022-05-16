using FeiraSP.Web.API.TEST.Util;
using FeiraSP.WEB.API.Model.DTO;
using FeiraSP.WEB.API.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraSP.Web.API.TEST.ServiceFake
{
    internal class FeiraServiceFake : IFeiraService
    {
        private readonly List<FeiraRequestDto> _lstFeriasResp;

        public FeiraServiceFake()
        {
             _lstFeriasResp = getDataFromJson();
        }

        private List<FeiraRequestDto> getDataFromJson()
        {

            List<FeiraRequestDto> items;
            using (StreamReader r = new StreamReader("FeriasForTests.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<FeiraRequestDto>>(json);
            } 
            return items;

        }

       

        private FeiraResponseDto convertReqToResp(FeiraRequestDto feiraDto)
        {
            var feiraResp = new FeiraResponseDto()
            {
                Id = feiraDto.Id,
                AreaPrefeitura = feiraDto.AreaPrefeitura,
                Longitude = feiraDto.Longitude,
                Latitude = feiraDto.Latitude,
                SetCens = feiraDto.SetCens,
                Bairro = feiraDto.Bairro,
                DistridoID = feiraDto.DistridoID,
                Logradouro = feiraDto.Logradouro,
                Nome = feiraDto.Nome,
                Numero = feiraDto.Numero,
                Referencia = feiraDto.Referencia,
                Regiao5 = feiraDto.Regiao5,
                Regiao8 = feiraDto.Regiao8,
                Registro = feiraDto.Registro,
                SubPrefeituraID = feiraDto.SubPrefeituraID
            };
            return feiraResp;
        }

        private FeiraRequestDto convertRespToReq(FeiraResponseDto feiraDto)
        {
            var feiraReq = new FeiraRequestDto()
            {
                Id = feiraDto.Id,
                AreaPrefeitura = feiraDto.AreaPrefeitura,
                Longitude = feiraDto.Longitude,
                Latitude = feiraDto.Latitude,
                SetCens = feiraDto.SetCens,
                Bairro = feiraDto.Bairro,
                DistridoID = feiraDto.DistridoID,
                Logradouro = feiraDto.Logradouro,
                Nome = feiraDto.Nome,
                Numero = feiraDto.Numero,
                Referencia = feiraDto.Referencia,
                Regiao5 = feiraDto.Regiao5,
                Regiao8 = feiraDto.Regiao8,
                Registro = feiraDto.Registro,
                SubPrefeituraID = feiraDto.SubPrefeituraID
            };
            return feiraReq;
        }


        public bool Atualizar(FeiraRequestDto feiraDto)
        {
            throw new NotImplementedException();
        }

        public FeiraResponseDto BuscarPorId(int id)
        {
            var item = _lstFeriasResp.FirstOrDefault(a => a.Id == id);
            if(item != null)
               return convertReqToResp(item);

            throw new KeyNotFoundException(String.Format("Id {0} não encontrado na base", id));
        }

        public int Criar(FeiraRequestDto feiraDto)
        {

            feiraDto.Id = FeiraUtil.CriarID();
            _lstFeriasResp.Add(feiraDto);
            return feiraDto.Id;
           
        }

        public bool Excluir(int id)
        {
            var existing = _lstFeriasResp.First(a => a.Id == id);
            return _lstFeriasResp.Remove(existing);
        }

        public IList<FeiraResponseDto> PesquisaFeira(int? distritoId, string? regiao5, string? nome, string? bairro)
        {
           var listResultado = _lstFeriasResp.Where(a => 
                (a.DistridoID == distritoId || distritoId == null) || 
                (a.Regiao5.Contains(regiao5) || regiao5 == null) || 
                (a.Nome.Contains(nome) || nome == null ) ||
                (a.Bairro.Contains(nome) || bairro == null)).ToList();

            List<FeiraResponseDto> result = listResultado.Select(f => convertReqToResp(f)).ToList();
            return result;
        }
    }
}
