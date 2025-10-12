using HelpPoint.Common;
using HelpPoint.Infrastructure.Models.Support;

namespace HelpPoint.Features.Support;

public interface ISupportRequestRepository : IRepository<SupportRequest>
{
    public Task<SupportRequest?> CreateSupportRequestAsync(SupportRequest supportRequest);
    public Task<List<SupportRequest>> GetAllPendingAsync();
}

