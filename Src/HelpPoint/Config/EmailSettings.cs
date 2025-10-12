namespace HelpPoint.Config;

public class EmailSettings
{
    public string Host { get; set; } = String.Empty;
    public int Port { get; set; } = 0;
    public string UserName { get; set; } = String.Empty;
    public string PassWord { get; set; } = String.Empty;
    public int TimeOut { get; set; } = 0;
}
