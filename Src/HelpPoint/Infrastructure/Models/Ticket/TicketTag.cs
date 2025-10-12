using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("TicketTags", Schema = "Ticket")]
[PrimaryKey(nameof(TicketId), nameof(TagId))]
public class TicketTag
{
    [Key, Column(Order = 0)]
    public Guid TicketId { get; set; }

    [Key, Column(Order = 1)]
    [MaxLength(36)]
    public Guid TagId { get; set; }

    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; } = null!;

    [ForeignKey("TagId")]
    public Tag Tag { get; set; } = null!;
}
