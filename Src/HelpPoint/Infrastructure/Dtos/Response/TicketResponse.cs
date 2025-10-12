namespace HelpPoint.Infrastructure.Dtos.Response;

public class TicketResponse
{
    public Guid Id { get; set; }
    public int? OrdenEnTablero { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public LookUpResponse Estado { get; set; } = null!;
    public LookUpResponse Tipo { get; set; } = null!;
    public LookUpResponse Prioridad { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaCierre { get; set; }
    public Guid? SupportRequestId { get; set; }
    public UserLookUpResponse CreatedBy { get; set; } = null!;
    public List<CommentResponse?> Comments { get; set; } = null!;
}

public class UserLookUpResponse
{
    public Guid CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = null!;
}

public class LookUpResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}

public class CommentResponse
{
    public Guid Id { get; set; }
    public UserLookUpResponse User { get; set; } = null!;
    public string Comentario { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
