namespace HelpPoint.Infrastructure.Dtos.Response;

public class SupportRequestResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string NombreEstado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaResolucion { get; set; }
    public Guid? EmpleadoId { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool TokenVerificacion { get; set; }
}
