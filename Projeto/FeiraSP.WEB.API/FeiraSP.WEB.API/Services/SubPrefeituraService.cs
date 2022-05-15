using FeiraSP.WEB.API.Data;
using FeiraSP.WEB.API.Model;
using FeiraSP.WEB.API.Model.DTO;

namespace FeiraSP.WEB.API.Services
{
    public class SubPrefeituraService : ISubPrefeituraService
    {
        private readonly FeiraContext feiraContext;

        public SubPrefeituraService(FeiraContext context) => this.feiraContext = context;

        public IList<SubPrefeituraResponseDto> Pesquisa()
        {
            var itensFeira = feiraContext.SubPrefeituras.ToList();

            List<SubPrefeituraResponseDto> result = itensFeira.Select(f => convertEntityToDto(f)).ToList();
            return result;
        }

        private SubPrefeituraResponseDto convertEntityToDto(SubPrefeitura f)
        {
            var dto = new SubPrefeituraResponseDto
            {
                Id = f.Id,
                Nome = f.Nome
            };
            return dto;
        }
    }
}
