using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VariacaoAtivos.Domain.Models;

public class ActiveVariation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int Dia { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public decimal VariacaoDMenosUM { get; set; }
    public decimal VariacaoPrimeiraData { get; set; }
}