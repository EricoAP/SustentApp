namespace SustentApp.Domain.Emails.Repositories.Abstractions;

public interface IEmailsRepository
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
}
