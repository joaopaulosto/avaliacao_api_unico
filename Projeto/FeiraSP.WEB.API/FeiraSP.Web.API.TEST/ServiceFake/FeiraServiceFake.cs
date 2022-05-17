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
            using (StreamReader r = new StreamReader("FeirasForTests.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<FeiraRequestDto>>(json);
            }
            return items;

        }


        public bool Atualizar(FeiraRequestDto feiraDto)
        {
            if (feiraDto == null || feiraDto.Id == 0)
            {
                throw new NullReferenceException();
            }

            var feiraExistente = _lstFeriasResp.Where(a => a.Id == feiraDto.Id).FirstOrDefault();

            if(feiraExistente == null)
            {
                throw new KeyNotFoundException();
            }

            _lstFeriasResp.Remove(feiraExistente);

            var atualizou = false;
            if(feiraExistente.Nome != feiraDto.Nome)
            {
                feiraExistente.Nome = feiraDto.Nome;
                atualizou = true;

            }
            if (feiraExistente.Bairro != feiraDto.Bairro)
            {
                feiraExistente.Bairro = feiraDto.Bairro;
                atualizou = true;

            }
            _lstFeriasResp.Add(feiraExistente);

            return atualizou;

        }

        public FeiraResponseDto BuscarPorId(int id)
        {
            var item = _lstFeriasResp.FirstOrDefault(a => a.Id == id);
            if (item != null)
                return FeiraUtil.convertReqToResp(item);

            throw new KeyNotFoundException(String.Format("Id {0} não encontrado na base", id));
        }

        public FeiraResponseDto Criar(FeiraRequestDto feiraDto)
        {
            feiraDto.Id = FeiraUtil.CriarID();
            _lstFeriasResp.Add(feiraDto);
            var feiraCriada = FeiraUtil.convertReqToResp(feiraDto);
            return feiraCriada;

        }

        public bool Excluir(int id)
        {
            var existing = _lstFeriasResp.Where(a => a.Id == id).FirstOrDefault();
            if (existing != null)
                return _lstFeriasResp.Remove(existing);

            return false;


        }

        public IList<FeiraResponseDto> PesquisaFeira(int? distritoId, string? regiao5, string? nome, string? bairro)
        {

            if ((distritoId == null || distritoId == 0) &&
                          String.IsNullOrEmpty(regiao5) &&
                                  String.IsNullOrEmpty(nome) &&
                                      String.IsNullOrEmpty(bairro))
                throw new Exception("A pequisa necessita ao menos de um campo (distrito|regiao5|nome|bairro)");


            var listResultado = _lstFeriasResp.Where(a =>
                (a.DistridoID == distritoId || distritoId == null) ||
                (a.Regiao5.Contains(regiao5) || regiao5 == null) ||
                (a.Nome.Contains(nome) || nome == null) ||
                (a.Bairro.Contains(nome) || bairro == null)).ToList();

            List<FeiraResponseDto> result = listResultado.Select(f => FeiraUtil.convertReqToResp(f)).ToList();
            return result;
        }
    }
}
