using FeiraSP.WEB.API.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraSP.Web.API.TEST.Util
{
    internal class FeiraUtil
    {
        public static int CriarID()
        {            
            return CriarID(1000,05000);
        }

        public static int CriarID(int inicio, int final)
        {
            Random random = new Random();
            int value = random.Next(inicio, final);
            return value;
        }

        public static FeiraResponseDto convertReqToResp(FeiraRequestDto feiraDto)
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

        public static FeiraRequestDto convertRespToReq(FeiraResponseDto feiraDto)
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

    }
}
