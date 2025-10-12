using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpPoint.Infrastructure.Models.Users;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("Notificaciones", Schema = "Ticket")]
public class Notificacion
{
    [Key]public Guid Id { get; set; }

    public Guid? TicketId { get; set; }

    public Guid? UserId { get; set; }

    [Required] [MaxLength(50)] public string TipoNotificacion { get; set; } = string.Empty;

    [Required] public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

    [Required] public bool ExitoEnvio { get; set; } = false;

    public string? Mensaje { get; set; }

    [ForeignKey("TicketId")] public Ticket? Ticket { get; set; }

    [ForeignKey("UserId")] public User? User { get; set; }
}
