using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CertStore.Api.Models;

public class UserCertificate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string CommonName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RootCertificateId { get; set; } = string.Empty;
    public string CsrData { get; set; } = string.Empty;
    public string PemData { get; set; } = string.Empty;
    public DateTime SignedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    
}