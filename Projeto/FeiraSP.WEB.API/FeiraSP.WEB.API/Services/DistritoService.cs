using FeiraSP.WEB.API.Data;
using FeiraSP.WEB.API.Model;
using FeiraSP.WEB.API.Model.DTO;

namespace FeiraSP.WEB.API.Services
{
    public class DistritoService : IDistritoService
    {
        private readonly FeiraContext feiraContext;

        public DistritoService(FeiraContext context)
        {
            this.feiraContext = context;
        }
        public IList<DistritoResponseDto> Pesquisa()
        {
            var itensFeira = feiraContext.Distritos.ToList();

            List<DistritoResponseDto> result = itensFeira.Select(f => convertEntityToDto(f)).ToList();
            return result;
        }

        private DistritoResponseDto convertEntityToDto(Distrito f)
        {
            var dto = new DistritoResponseDto
            {
                Id = f.Id,
                Nome = f.Nome
            };
            return dto;
        }
    }
}
