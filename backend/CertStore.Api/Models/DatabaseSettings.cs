namespace CertStore.Api.Models;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string RootCertificatesCollection { get; set; } = string.Empty;
    public string UserCertificatesCollection { get; set; } = string.Empty;
}