using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Monetra.Domain.BackOffice.Interfaces.Services;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.Service;

public class ServiceEmail : IServiceEmail
{
    private readonly IConfiguration _configuration;

    public ServiceEmail(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> TrySendEmail(EmailSending email)
    {
        try
        {
            await SendEmailAsync(email);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private async Task SendEmailAsync(EmailSending email)
    {
        var mensagem = new MimeMessage();
    
        mensagem.From.Add(new MailboxAddress("Monetra", _configuration["EmailConfig:Email"]));
        
        mensagem.To.Add(new MailboxAddress(email.ToName, email.ToAddress));
    
        mensagem.Subject = email.Menssage;
        mensagem.Body = new TextPart("html") { Text = email.Body };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            
            await client.AuthenticateAsync(_configuration["EmailConfig:Email"], _configuration["EmailConfig:key"]);

            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }
    }
}