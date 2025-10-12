using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelpPoint.Infrastructure.Dtos.Response;

namespace HelpPoint.Features.Catalogo;

[Authorize]
[ApiController]
[Route("api/v1/catalogo")]
public class CatalogoController(ICatalogoService catalogoService) : ControllerBase
{
    private readonly ICatalogoService _catalogoService = catalogoService;

    [HttpGet("estados")]
    public async Task<ActionResult<List<PSelectableResponse>>> GetEstados()
    {
        var estados = await _catalogoService.GetEstados();
        return Ok(estados);
    }

    [HttpGet("prioridades")]
    public async Task<ActionResult<List<PSelectableResponse>>> GetPrioridades()
    {
        var estados = await _catalogoService.GetPrioridades();
        return Ok(estados);
    }
}
