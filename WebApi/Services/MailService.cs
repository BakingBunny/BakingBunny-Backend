﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Settings;

namespace WebApi.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        /// <summary>
        /// This method sends a custom cake order details to bakingbunny.yyc@gmail.com
        /// </summary>
        /// <param name="customOrder"></param>
        /// <returns></returns>
        public async Task SendInternalEmailAsync(CustomOrder customOrder)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse("bakingbunny.yyc@gmail.com")); // Change the email address for test if needed.
            email.Subject = "A custom cake order from " + customOrder.User.Firstname + " " + customOrder.User.Lastname;
            var builder = new BodyBuilder();
            builder.HtmlBody = "<b>Name</b>: " + customOrder.User.Firstname + " " + customOrder.User.Lastname + "<br>" +
                "<b>Email</b>: " + customOrder.User.Email + "<br>" +
                "<b>Phone</b>: " + customOrder.User.Phone + "<br>" +
                "<b>Delivery</b>: " + (customOrder.Delivery ? "Yes<br>" : "No<br>") +
                "<b>Address</b>: " + customOrder.User.Address + "<br>" +
                "<b>PostalCode</b>: " + customOrder.User.PostalCode + "<br>" +
                "<b>City</b>: " + customOrder.User.City + "<br>" +
                "<b>Example Image</b>: " + customOrder.ExampleImage + "<br>" + // Need to review this line.
                "<b>Message on the cake</b>: " + customOrder.Message + "<br>" +
                "<b>Comment</b>: " + customOrder.Comment + "<br>";
            //builder.TextBody = "";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        /// <summary>
        /// This method sends a confirmation email to the client.
        /// </summary>
        /// <param name="customOrder"></param>
        /// <returns></returns>
        public async Task SendEmailToClientAsync(CustomOrder customOrder)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
#if(DEBUG)
            email.To.Add(MailboxAddress.Parse("bakingbunny.yyc@gmail.com")); // Change the email address for test if needed.
#else
            email.To.Add(MailboxAddress.Parse(customOrder.User.Email));
#endif
            email.Subject = "Thank you for your custom cake order";
            var builder = new BodyBuilder();
            builder.HtmlBody = "Hi " + customOrder.User.Firstname + " " + customOrder.User.Lastname + ",<br><br>" +
                "We've received your custom cake order and will contact you as soon as your cake is ready.<br><br>" +
                "Please feel free to reach us at <a href='mailto:bakingbunny.yyc@gmail.com'>bakingbunny.yyc@gmail.com</a>.<br><br>" +
                "Best regards,<br>" +
                "Baking Bunny";
            builder.TextBody = "Hi " + customOrder.User.Firstname + " " + customOrder.User.Lastname + "," +
                "We've received your custom cake order and will contact you as soon as your cake is ready." +
                "Please feel free to reach us at bakingbunny.yyc@gmail.com." +
                "Best regards," +
                "Baking Bunny";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);            
            smtp.Disconnect(true);
        }
    }
}