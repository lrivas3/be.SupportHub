namespace HelpPoint.Infrastructure.Dtos.Email;

public class EmailDto
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = null!;
}
