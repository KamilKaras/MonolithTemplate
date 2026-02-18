namespace MonolithTemplate.Notifications.Domain.Emails;

public sealed class SmtpSettings
{
    public SmtpSettings(
        string server,
        int port,
        string senderName,
        string senderEmail,
        string userName,
        string password,
        string security)
    {
        Server = server;
        Port = port;
        SenderName = senderName;
        SenderEmail = senderEmail;
        UserName = userName;
        Password = password;
        Security = security;

    }
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Security { get; }

}
