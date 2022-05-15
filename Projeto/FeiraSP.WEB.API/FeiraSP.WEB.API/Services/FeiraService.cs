using FeiraSP.WEB.API.CustomErrors;
using FeiraSP.WEB.API.Data;
using FeiraSP.WEB.API.Model;
using FeiraSP.WEB.API.Model.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FeiraSP.WEB.API.Services
{
    public class FeiraService : IFeiraService
    {
        private readonly FeiraContext feiraContext;
        public FeiraService(FeiraContext context)
        {
            this.feiraContext = context;
        }

        private Feira convertDtoToEntity(FeiraRequestDto feiraDto)
        {
            var feiraEntity = new Feira()
            {
                AreaPrefeitura = feiraDto.AreaPrefeitura,
                Longitude = feiraDto.Longitude,
                Latitude = feiraDto.Latitude,
                SetCens = feiraDto.SetCens,
                Bairro = feiraDto.Bairro,
                DistritoID = feiraDto.DistridoID,
                Logradouro = feiraDto.Logradouro,
                Nome = feiraDto.Nome,
                Numero = feiraDto.Numero,
                Referencia = feiraDto.Referencia,
                Regiao5 = feiraDto.Regiao5,
                Regiao8 = feiraDto.Regiao8,
                Registro = feiraDto.Registro,
                SubPrefeituraID = feiraDto.SubPrefeituraID
            };
            return feiraEntity;
        }

        private FeiraResponseDto convertEntityToDto(Feira feiraEntity)
        {
            var feiraResponseDto = new FeiraResponseDto()
            {
                Id = feiraEntity.Id,
                AreaPrefeitura = feiraEntity.AreaPrefeitura,
                Longitude = feiraEntity.Longitude,
                Latitude = feiraEntity.Latitude,
                SetCens = feiraEntity.SetCens,
                Bairro = feiraEntity.Bairro,
                DistridoID = feiraEntity.DistritoID,
                Logradouro = feiraEntity.Logradouro,
                Nome = feiraEntity.Nome,
                Numero = feiraEntity.Numero,
                Referencia = feiraEntity.Referencia,
                Regiao5 = feiraEntity.Regiao5,
                Regiao8 = feiraEntity.Regiao8,
                Registro = feiraEntity.Registro,
                SubPrefeituraID = feiraEntity.SubPrefeituraID,



            };
            if (feiraEntity.Distrito != null)
            {
                feiraResponseDto.Distrito = new DistritoResponseDto
                {
                    Id = feiraEntity.Distrito.Id,
                    Nome = feiraEntity.Distrito.Nome
                };
            }

            if (feiraEntity.SubPrefeitura != null) { 
                feiraResponseDto.SubPrefeitura = new SubPrefeituraResponseDto
                {
                    Id = feiraEntity.SubPrefeitura.Id,
                    Nome = feiraEntity.SubPrefeitura.Nome
                };
            }

            return feiraResponseDto;
        }

    public int Criar(FeiraRequestDto feiraDto)
    {

        var feira_db = convertDtoToEntity(feiraDto);
        this.feiraContext.Add(feira_db);
        this.feiraContext.SaveChanges();
        return feira_db.Id;


    }

    public FeiraResponseDto BuscarPorId(int id)
    {
        var feira = buscarPorId(id);
        if (feira == null)
            throw new KeyNotFoundException(String.Format("Id {0} não encontrado na base", id));

        var feiraRespDto = convertEntityToDto(feira);
        return feiraRespDto;

    }

    private Feira? buscarPorId(int id)
    {
        Feira? feira = feiraContext.Feiras.Include(d => d.Distrito).Include(s => s.SubPrefeitura).FirstOrDefault(f => f.Id == id);
        return feira;
    }

    public bool Atualizar(FeiraRequestDto feiraDto)
    {
        var index = 0;
        if (feiraDto.Id <= 0)
            throw new FeiraException("O campo ID é obrigatório para alteração da feira");


        var feiraEntity = buscarPorId(feiraDto.Id);

        if (feiraEntity == null)
            throw new KeyNotFoundException(String.Format("Id {0} não encontrado para atualizacao", feiraDto.Id));


        if (feiraDto.AreaPrefeitura != feiraEntity.AreaPrefeitura)
        {
            feiraEntity.AreaPrefeitura = feiraDto.AreaPrefeitura;
            index++;
        }

        if (feiraDto.SubPrefeituraID != feiraEntity.SubPrefeituraID)
        {
            feiraEntity.SubPrefeituraID = feiraDto.SubPrefeituraID;
            index++;
        }

        if (feiraDto.Longitude != feiraEntity.Longitude)
        {
            feiraEntity.Longitude = feiraDto.Longitude;
            index++;
        }

        if (feiraDto.Latitude != feiraEntity.Latitude)
        {
            feiraEntity.Latitude = feiraDto.Latitude;
            index++;
        }


        if (feiraDto.SetCens != feiraEntity.SetCens)
        {
            feiraEntity.SetCens = feiraDto.SetCens;
            index++;

        }

        if (!feiraDto.Bairro.Equals(feiraEntity.Bairro))
        {
            feiraEntity.Bairro = feiraDto.Bairro;
            index++;
        }

        if (feiraDto.DistridoID != feiraEntity.DistritoID)
        {
            feiraEntity.DistritoID = feiraDto.DistridoID;
            index++;
        }

        if (!feiraDto.Logradouro.Equals(feiraEntity.Logradouro))
        {
            feiraEntity.Logradouro = feiraDto.Logradouro;
            index++;
        }

        if (!feiraDto.Nome.Equals(feiraEntity.Nome))
        {
            feiraEntity.Nome = feiraDto.Nome;
            index++;
        }

        if (!feiraDto.Numero.Equals(feiraEntity.Numero))
        {
            feiraEntity.Numero = feiraDto.Numero;
            index++;
        }


        if (!feiraDto.Referencia.Equals(feiraEntity.Referencia))
        {
            feiraEntity.Referencia = feiraDto.Referencia;
            index++;
        }

        if (!feiraDto.Regiao5.Equals(feiraEntity.Regiao5))
        {
            feiraEntity.Regiao5 = feiraDto.Regiao5;
            index++;
        }

        if (!feiraDto.Regiao8.Equals(feiraEntity.Regiao8))
        {
            feiraEntity.Regiao8 = feiraDto.Regiao8;
            index++;
        }

        if (!feiraDto.Registro.Equals(feiraEntity.Registro))
        {
            feiraEntity.Registro = feiraDto.Registro;
            index++;
        }

        if (index > 0)
            feiraContext.SaveChanges();

        return index > 0;


    }

    public bool Excluir(int id)
    {
        var feiraEntity = buscarPorId(id);

        if (feiraEntity == null)
            throw new KeyNotFoundException(String.Format("Id {0} não encontrado para remoção", id));


        feiraContext.Remove(feiraEntity);
        feiraContext.SaveChanges();

        return true;


    }

    public IList<FeiraResponseDto> PesquisaFeira(int? distritoId, string? regiao5, string? nome, string? bairro)
    {
        validaParametrosPesquisa(distritoId, regiao5, nome, bairro);


        var paramDistrito = new SqlParameter("distritoId", distritoId == null ? DBNull.Value : distritoId);
        var paramRegiao5 = new SqlParameter("regiao5", regiao5 == null ? DBNull.Value : regiao5);
        var paramNome = new SqlParameter("nome", nome == null ? DBNull.Value : nome);
        var paramBairro = new SqlParameter("bairro", bairro == null ? DBNull.Value : bairro);


        var itensFeira = feiraContext.Feiras                
                .FromSqlRaw("EXECUTE dbo.SEL_FEIRA @distritoId, @regiao5, @nome, @bairro ", paramDistrito, paramRegiao5, paramNome, paramBairro)                
                .ToList();            
        List<FeiraResponseDto> result = itensFeira.Select(f => convertEntityToDto(f)).ToList();
        return result;

    }

    private void validaParametrosPesquisa(int? distritoId, string? regiao5, string? nome, string? bairro)
    {
        if ((distritoId == null || distritoId == 0) &&
                    String.IsNullOrEmpty(regiao5) &&
                            String.IsNullOrEmpty(nome) &&
                                String.IsNullOrEmpty(bairro))
            throw new FeiraException("A pequisa necessita ao menos de um campo (distrito|regiao5|nome|bairro)");


    }


}
}
