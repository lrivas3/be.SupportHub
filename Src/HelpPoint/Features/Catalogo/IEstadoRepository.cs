using HelpPoint.Common;
using HelpPoint.Infrastructure.Models.Ticket;

namespace HelpPoint.Features.Catalogo;

public interface IEstadoRepository : IRepository<Estado>
{
    public Task<List<Estado>> GetAllEstadosAsync();
    public Task<List<Prioridad>> GetAllPrioridadesAsync();
}
