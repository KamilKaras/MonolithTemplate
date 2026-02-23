namespace MonolithTemplate.Notifications.Domain.Emails;

public sealed class SmtpSettings
{
    public string Server { get; set; } = "";
    public int Port { get; set; }
    public string SenderName { get; set; } = "";
    public string SenderEmail { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string Security { get; set; } = "";
}