using System.ComponentModel.DataAnnotations;

namespace HelpPoint.Infrastructure.Dtos.Request;

public class TicketRequest
{
    public int? OrdenEnTablero { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    [Required]
    public int EstadoId { get; set; }
    [Required]
    public int TipoId { get; set; }
    [Required]
    public int PrioridadId { get; set; }
    public Guid? SupportRequestId { get; set; }
}
