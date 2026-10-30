using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MailMessage { 
            From= new MailAddress("noreply@delex.com")
        };
        emailMessage.To.Add(new MailAddress(email, email));
        emailMessage.Subject = subject;

        emailMessage.Body = message;

        using var client = new SmtpClient();

        client.SendAsync(emailMessage, subject);

        emailMessage.Dispose();
        client.Dispose();
    }
}
