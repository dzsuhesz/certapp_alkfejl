using CertStore.Api.Models;
using CertStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CertStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCertificatesController : ControllerBase
{
    private readonly CertificateService _service;

    public UserCertificatesController(CertificateService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var certs = await _service.GetUserCertsAsync(page, pageSize);
        var total = await _service.CountUserCertsAsync();
        return Ok(new { data = certs, total, page, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var cert = await _service.GetUserCertAsync(id);
        if (cert is null) return NotFound();
        return Ok(cert);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCertificate cert)
    {
        cert.SignedAt = DateTime.UtcNow;
        var created = await _service.CreateUserCertAsync(cert);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteUserCertAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
    
}