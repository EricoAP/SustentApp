using Microsoft.Extensions.Configuration;
using SustentApp.Domain.Emails.Repositories.Abstractions;
using System.Net;
using System.Net.Mail;

namespace SustentApp.Infrastructure.Emails.Repositories;

public class EmailRepository : IEmailsRepository
{
    private readonly SmtpClient _smtpClient;
    private readonly MailAddress _fromAddress;

    public EmailRepository(IConfiguration configuration)
    {
        var host = configuration.GetSection("Smtp:Host").Value;
        var username = configuration.GetSection("Smtp:Username").Value;
        var password = configuration.GetSection("Smtp:Password").Value;
        int.TryParse(configuration.GetSection("Smtp:Port").Value, out var port);

        _smtpClient = new SmtpClient(host, port)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };

        _fromAddress = new MailAddress(
            address: configuration.GetSection("Smtp:FromAddress").Value, 
            displayName: configuration.GetSection("Smtp:DisplayName").Value
        );
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        MailAddress toAddress = new(to);
        MailMessage message = new(_fromAddress, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        await _smtpClient.SendMailAsync(message);
    }
}
