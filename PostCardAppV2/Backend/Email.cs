using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using MimeKit.Text;
using PostCardAppV2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable
namespace PostCardAppV2.Backend
{
    public class Email : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IServiceScopeFactory _service;

        public Email(IConfiguration config, IServiceScopeFactory service)
        {
            _config = config;
            _service = service;
        }

        private async Task sendMessage(string name, string subject, string address, string body, string? s)
         {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Quote System", _config["MailGun:ServerAddress"]));
            mail.To.Add(new MailboxAddress(name, address));
            mail.Cc.Add(new MailboxAddress("C", _config["MailGun:CEmail"]));
            mail.Subject = subject;
            var content = new Multipart("mixed");
            content.Add(new TextPart(TextFormat.Html)
            {
                Text = body
            });
            if (s != null)
            {
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
                var attachment = new MimePart("text/csv")
                {
                    Content = new MimeContent(stream),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Default,
                    FileName = "QuoteReport" + DateTime.Today + ".csv",
                };
                content.Add(attachment);
            }
            mail.Body = content;

            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailgun.org", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_config["MailGun:UserName"],
                _config["MailGun:Password"]);

                client.Send(mail);
                client.Disconnect(true);
            }
        }

        public void sendCsvRecycleEmail()
        {
            using (var scope = _service.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<PostCardAppContext>();
                TimeZoneInfo chicago = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                var oldQuotes = _context.Quotes.Include(x => x.Estimates).Where(x => x.CreatedOn.AddDays(1) < DateTime.UtcNow).ToList();

                var builder = new StringBuilder();
                builder.AppendLine("ID,CustomerName,Bleed,Paper,CardSize,Color,Quantity,Price,Created");
                foreach (var quote in oldQuotes)
                {
                    var QuoteInputs = $"{quote.ID},{quote.CustomerName},{quote.DisplayBleed()},{quote.Paper}" +
                        $",{quote.CardSize},{quote.Color}";
                    var offset = chicago.GetUtcOffset(quote.CreatedOn);
                    foreach (var est in quote.Estimates)
                    {
                        builder.AppendLine($"{QuoteInputs},{est.Quantity},{est.Price},{quote.CreatedOn + offset}");
                    }
                }
                _context.RemoveRange(oldQuotes);
                _context.SaveChanges();
                sendMessage(_config["MailGun:DonName"], "PDF", _config["MailGun:DonEmail"], "pdf File test", builder.ToString()).Wait();
            }
        }
    }
}
