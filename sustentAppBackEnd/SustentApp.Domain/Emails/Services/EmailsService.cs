using HandlebarsDotNet;
using SustentApp.Domain.Emails.Repositories.Abstractions;
using SustentApp.Domain.Emails.Services.Abstractions;
using System.Reflection;

namespace SustentApp.Domain.Emails.Services;

public class EmailsService : IEmailsService
{
    private readonly IEmailsRepository _emailsRepository;

    public EmailsService(IEmailsRepository emailsRepository)
    {
        _emailsRepository = emailsRepository;
    }

    public async Task SendEmailAsync(string to, string subject, string templateFile, object data, bool isHtml = false)
    {
        var body = await TransformTemplateAsync(templateFile, data);
        await _emailsRepository.SendEmailAsync(to, subject, body, isHtml);
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        await _emailsRepository.SendEmailAsync(to, subject, body, isHtml);
    }

    private async Task<string> TransformTemplateAsync(string templateFile, object data)
    {
        var templateText = await GetTemplateFromAssemblyAsync(templateFile);

        var template = Handlebars.Compile(templateText);

        return template(data);
    }

    private async Task<string> GetTemplateFromAssemblyAsync(string templateFile)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using Stream stream = assembly.GetManifestResourceStream(templateFile);
        using StreamReader reader = new(stream);

        return await reader.ReadToEndAsync();
    }
}
