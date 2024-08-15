namespace SustentApp.Domain.Emails.Services.Abstractions;

public interface IEmailsService
{
    Task SendEmailAsync(string to, string subject, string templateFile, object data, bool isHtml = false);
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
}
