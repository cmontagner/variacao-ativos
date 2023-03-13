using VariacaoAtivos.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VariacaoAtivos.Infra.Config;
using VariacaoAtivos.Infra.Repositories.Interfaces;

namespace VariacaoAtivos.Infra.Repositories;

public class ActiveVariationRepository: IActiveVariationRepository
{
    private readonly IMongoCollection<ActiveVariation> _activeVariationCollection;

    public ActiveVariationRepository(
        IOptions<ActiveVariationDatabaseSettings> activeVariationDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            activeVariationDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            activeVariationDatabaseSettings.Value.DatabaseName);

        _activeVariationCollection = mongoDatabase.GetCollection<ActiveVariation>(
            activeVariationDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<ActiveVariation>> GetAsync() =>
        await _activeVariationCollection.Find(_ => true).ToListAsync();

    public async Task<ActiveVariation?> GetAsync(string id) =>
        await _activeVariationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ActiveVariation newVariation) =>
        await _activeVariationCollection.InsertOneAsync(newVariation);

    public async Task UpdateAsync(string id, ActiveVariation updatedVariation) =>
        await _activeVariationCollection.ReplaceOneAsync(x => x.Id == id, updatedVariation);

    public async Task RemoveAsync(string id) =>
        await _activeVariationCollection.DeleteOneAsync(x => x.Id == id);
}