using FeiraSP.WEB.API.Model.DTO;

namespace FeiraSP.WEB.API.Services
{
    public interface IDistritoService
    {
        IList<DistritoResponseDto> Pesquisa();
    }
}
