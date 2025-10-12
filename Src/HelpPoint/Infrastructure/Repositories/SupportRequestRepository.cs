using HelpPoint.Features.Support;
using HelpPoint.Infrastructure.DataBase;
using HelpPoint.Infrastructure.Models.Support;
using Microsoft.EntityFrameworkCore;

namespace HelpPoint.Infrastructure.Repositories;

public class SupportRequestRepository(HelpPointDbContext context) : Repository<SupportRequest>(context),ISupportRequestRepository
{
    private const int ESTADO_PENDING = 1;
    public async Task<SupportRequest?> CreateSupportRequestAsync(SupportRequest supportRequest)
    {
        var entry = await context.SupportRequests.AddAsync(supportRequest);
        _ = await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<List<SupportRequest>> GetAllPendingAsync() =>
        await context.SupportRequests
            .Where(x => x.EstadoId == ESTADO_PENDING)
            .OrderBy(x => x.FechaCreacion)
            .ToListAsync();
}
