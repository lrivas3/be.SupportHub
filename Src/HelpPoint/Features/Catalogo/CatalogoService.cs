using AutoMapper;
using HelpPoint.Infrastructure.Dtos.Response;
using HelpPoint.Infrastructure.Models.Ticket;
using HelpPoint.Infrastructure.Repositories;

namespace HelpPoint.Features.Catalogo;

public class CatalogoService(IEstadoRepository estadoRepository) : ICatalogoService
{
    private readonly IEstadoRepository _estadoRepository = estadoRepository;

    public async Task<List<PSelectableResponse>> GetEstados()
    {
        var estados = await _estadoRepository.GetAllEstadosAsync();
        return [.. estados.Select(e => new PSelectableResponse { Label = e.NombreEstado, Value = e.Id })];
    }

    public async Task<List<PSelectableResponse>> GetPrioridades()
    {
        var prioridades = await _estadoRepository.GetAllPrioridadesAsync();
        return [.. prioridades.Select(e => new PSelectableResponse { Label = e.Nombre, Value = e.Id })];
    }
}
