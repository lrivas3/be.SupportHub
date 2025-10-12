namespace HelpPoint.Infrastructure.Dtos.Request;

public class ReorderTicketDto
{
    public string TicketId { get; set; } = null!;
    public int EstadoId { get; set; }
    public int? OrdenEnTablero { get; set; }
}

public class ReorderPayload
{
    public List<ReorderTicketDto> Tickets { get; set; } = null!;
}
