using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using FluentEmail.Mailgun;
using FluentEmail.Core;

namespace ProdGameApplication.Models.Auth
{
    public class EmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var domain = _configuration.GetSection("EmailCredential").GetSection("Domain").Value;
            var apiKey = _configuration.GetSection("EmailCredential").GetSection("ApiKey").Value;
            var sender = new MailgunSender(domain, apiKey);

            Email.DefaultSender = sender;

            var emailObj = Email
                .From("testcardgame13@gmail.com")
                .To(email)
                .Subject(subject)
                .Body(htmlMessage);

            var response = emailObj.SendAsync();

            //_logger.LogInformation(response.Result.Successful
            //                    ? $"Email to {email} queued successfully!"
            //                    : $"Failure Email to {email}");

            return response;
        }
    }
}
