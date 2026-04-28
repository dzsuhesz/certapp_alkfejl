using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CertStore.Api.Models;

public class RootCertificate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string CommonName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public string PemData { get; set; } = string.Empty;
    
}