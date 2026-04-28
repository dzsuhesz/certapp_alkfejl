using CertStore.Api.Models;
using CertStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CertStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RootCertificatesController : ControllerBase
{
    private readonly CertificateService _service;

    public RootCertificatesController(CertificateService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var certs = await _service.GetRootCertsAsync(page, pageSize);
        var total = await _service.CountRootCertsAsync();
        return Ok(new { data = certs, total, page, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var cert = await _service.GetRootCertAsync(id);
        if (cert is null) return NotFound();
        return Ok(cert);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RootCertificate cert)
    {
        cert.CreatedAt = DateTime.UtcNow;
        var created = await _service.CreateRootCertAsync(cert);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteRootCertAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}