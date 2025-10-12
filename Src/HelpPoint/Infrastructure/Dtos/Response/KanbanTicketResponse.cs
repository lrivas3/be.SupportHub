namespace HelpPoint.Infrastructure.Dtos.Response;

public class KanbanTicketResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public LookUpResponse Estado { get; set; } = null!;
    public LookUpResponse Tipo { get; set; } = null!;
    public LookUpResponse Prioridad { get; set; } = null!;
    public DateTime? CreationDate { get; set; }
    public DateTime? ClosureDate { get; set; }
    public int OrderInBoard { get; set; }
    public List<string>? Tags { get; set; } = [];
    public int? Progress { get; set; }
    public string? Checklist { get; set; }
    public int? Attachments { get; set; }
    public List<string> Avatars { get; set; } = [];
    public Guid? SupportRequestId { get; set; }
    public UserLookUpResponse CreatedBy { get; set; } = null!;
    public List<CommentResponse>? Comments { get; set; }
}
