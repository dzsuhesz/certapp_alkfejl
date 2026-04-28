using CertStore.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CertStore.Api.Services;

public class CertificateService
{
    private readonly IMongoCollection<RootCertificate> _rootCerts;
    private readonly IMongoCollection<UserCertificate> _userCerts;

    public CertificateService(IOptions<DatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var db = client.GetDatabase(settings.Value.DatabaseName);
        _rootCerts = db.GetCollection<RootCertificate>(settings.Value.RootCertificatesCollection);
        _userCerts = db.GetCollection<UserCertificate>(settings.Value.UserCertificatesCollection);
    }
// Root Certificates
    public async Task<List<RootCertificate>> GetRootCertsAsync(int page, int pageSize) =>
        await _rootCerts.Find(_ => true)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

    public async Task<long> CountRootCertsAsync() =>
        await _rootCerts.CountDocumentsAsync(_ => true);

    public async Task<RootCertificate?> GetRootCertAsync(string id) =>
        await _rootCerts.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<RootCertificate> CreateRootCertAsync(RootCertificate cert)
    {
        await _rootCerts.InsertOneAsync(cert);
        return cert;
    }

    public async Task<bool> DeleteRootCertAsync(string id)
    {
        var result = await _rootCerts.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }
// User Certificates
    // --- User Certificates ---
    public async Task<List<UserCertificate>> GetUserCertsAsync(int page, int pageSize) =>
        await _userCerts.Find(_ => true)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

    public async Task<long> CountUserCertsAsync() =>
        await _userCerts.CountDocumentsAsync(_ => true);

    public async Task<UserCertificate?> GetUserCertAsync(string id) =>
        await _userCerts.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<UserCertificate> CreateUserCertAsync(UserCertificate cert)
    {
        await _userCerts.InsertOneAsync(cert);
        return cert;
    }

    public async Task<bool> DeleteUserCertAsync(string id)
    {
        var result = await _userCerts.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }
}