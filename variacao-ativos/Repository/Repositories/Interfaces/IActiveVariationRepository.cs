using System.Threading.Tasks;
using VariacaoAtivos.Domain.Models;

namespace VariacaoAtivos.Infra.Repositories.Interfaces
{
    public interface IActiveVariationRepository
    {
        Task<List<ActiveVariation>> GetAsync();
        Task<ActiveVariation?> GetAsync(string id);
        Task CreateAsync(ActiveVariation newVariation);
        Task UpdateAsync(string id, ActiveVariation updatedVariation);
        Task RemoveAsync(string id);
    }
}