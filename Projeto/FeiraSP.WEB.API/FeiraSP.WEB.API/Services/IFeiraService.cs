using FeiraSP.WEB.API.Model.DTO;
using System.Collections;

namespace FeiraSP.WEB.API.Services
{
    public interface IFeiraService
    {
        int Criar(FeiraRequestDto feiraDto);

        FeiraResponseDto BuscarPorId(int id);

        bool Atualizar(FeiraRequestDto feiraDto);

        bool Excluir(int id);
        IList<FeiraResponseDto> PesquisaFeira(int? distritoId, string? regiao5, string? nome, string? bairro);
    }
}
