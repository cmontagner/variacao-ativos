using System;
using VariacaoAtivos.Domain.Models;
using VariacaoAtivos.Domain.Response;

namespace VariacaoAtivos.Service.Services.Interfaces
{
    public interface IActiveVariationService
    {
        Task<ApiResponse<List<ActiveVariation>>> GetActiveVariation();
        List<ActiveVariation> FillActiveVariatiosList(YahooActiveVariation yahooActiveVariation);
        Task<ApiResponse<List<ActiveVariation>>> CreateActiveVariation();
        Task InsertActiveVariation(List<ActiveVariation> variations);

    }
}
