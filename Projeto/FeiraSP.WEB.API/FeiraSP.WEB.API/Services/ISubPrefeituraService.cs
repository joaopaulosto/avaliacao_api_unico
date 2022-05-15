using FeiraSP.WEB.API.Model.DTO;

namespace FeiraSP.WEB.API.Services
{
    public interface ISubPrefeituraService
    {
        IList<SubPrefeituraResponseDto> Pesquisa();
    }
}
