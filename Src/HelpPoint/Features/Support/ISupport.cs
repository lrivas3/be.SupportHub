using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Dtos.Response;

namespace HelpPoint.Features.Support;

public interface ISupport
{
    public Task<SupportRequestResponse> CreateSupportRequestAsync(SupportRequestRequest request);
    public Task<SupportRequestResponse> UpdateSupportRequestAsync(SupportRequestUpdateRequest request, Guid id);
    public Task<SupportRequestResponse> GetSupportRequestAsync(Guid requestId);
    public Task<List<SupportRequestResponse>> ListSupportRequestsAsync();
    public Task<bool> DeleteSupportRequestAsync(Guid requestId);
}
