using MailKit.Net.Smtp;
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
        public async Task SendInternalEmailCustomAsync(CustomOrder customOrder)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse("bakingbunny.yyc@gmail.com")); // Change the email address for test if needed.
            email.Subject = "A custom cake order from " + customOrder.User.Firstname + " " + customOrder.User.Lastname;
            var builder = new BodyBuilder();
            builder.HtmlBody = "<strong>Order#</b>: " + customOrder.Id + "<br>" +
                "<strong>Name</strong>: " + customOrder.User.Firstname + " " + customOrder.User.Lastname + "<br>" +
                "<strong>Email</strong>: " + customOrder.User.Email + "<br>" +
                "<strong>Phone</strong>: " + customOrder.User.Phone + "<br>" +
                "<strong>Delivery</strong>: " + (customOrder.Delivery ? "Yes<br>" : "No<br>") +
                "<strong>Address</strong>: " + customOrder.User.Address + "<br>" +
                "<strong>PostalCode</strong>: " + customOrder.User.PostalCode + "<br>" +
                "<strong>City</strong>: " + customOrder.User.City + "<br>" +
                "<strong>Example Image</strong>: " + customOrder.ExampleImage + "<br>" + // Need to review this line.
                "<strong>Message on the cake</strong>: " + customOrder.Message + "<br>" +
                "<strong>Comment</strong>: " + customOrder.Comment + "<br>";
            //builder.TextBody = "";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        /// <summary>
        /// This method sends the confirmation email of custom order to the client.
        /// </summary>
        /// <param name="customOrder"></param>
        /// <returns></returns>
        public async Task SendEmailToClientCustomAsync(CustomOrder customOrder)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
#if(DEBUG)
            email.To.Add(MailboxAddress.Parse("bakingbunny.yyc@gmail.com")); // Change the email address for test if needed.
#else
            email.To.Add(MailboxAddress.Parse(customOrder.User.Email));
#endif
            email.Subject = "Thank you for your custom cake order from Baking Bunny";
            var builder = new BodyBuilder();
            builder.HtmlBody = "Hi " + customOrder.User.Firstname + " " + customOrder.User.Lastname + ",<br><br>" +
                "Your order # is: <strong>" + customOrder.Id + "</strong><br><br>" +
                "We've received your custom cake order and will contact you as soon as your cake is ready.<br><br>" +
                "Please feel free to reach us at <a href='mailto:bakingbunny.yyc@gmail.com'>bakingbunny.yyc@gmail.com</a>.<br><br>" +
                "Best regards,<br>" +
                "Baking Bunny";
            builder.TextBody = "Hi " + customOrder.User.Firstname + " " + customOrder.User.Lastname + "," +
                "Your order # is: " + customOrder.Id +
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

        /// <summary>
        /// This method sends a regular order details to bakingbunny.yyc@gmail.com
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public async Task SendInternalEmailRegularAsync(OrderDetail orderDetail, int orderId, List<Product> products)
        {
            double subtotal = 0;
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse("ahnjaehwan@hotmail.com")); // Change the email address for test if needed.
            email.Subject = "A regular order from " + orderDetail.user.Firstname + " " + orderDetail.user.Lastname;
            var builder = new BodyBuilder();
            string htmlBodyString = "<strong>Order#</strong>: " + orderId + "<br>" +
                "<strong>Name</strong>: " + orderDetail.user.Firstname + " " + orderDetail.user.Lastname + "<br>" +
                "<strong>Email</strong>: " + orderDetail.user.Email + "<br>" +
                "<strong>Phone</strong>: " + orderDetail.user.Phone + "<br>" +
                "<strong>Delivery</strong>: " + (orderDetail.orderList.Delivery ? "Yes<br>" : "No<br>");

                if (orderDetail.orderList.Delivery)
                    htmlBodyString += "<strong>Delivery Date</strong>: " + orderDetail.orderList.PickupDeliveryDate + "<br>";
                else
                    htmlBodyString += "<strong>Pickup Date</strong>: " + orderDetail.orderList.PickupDeliveryDate + "<br>";

                htmlBodyString += "<strong>Address</strong>: " + orderDetail.user.Address + "<br>" +
                "<strong>PostalCode</strong>: " + orderDetail.user.PostalCode + "<br>" +
                "<strong>City</strong>: " + orderDetail.user.City + "<br><br>" +
                "The order details are as follows:<br><br>" +
                "<table>" +
                "<tr><th style='text-align: left;'>Product Name</th><th style='text-align: left;'>Quantity</th><th style='text-align: left;'>Price</th></tr>";

            foreach (SaleItem s in orderDetail.saleItems)
            {
                var p = products.Find(x => x.Id == s.ProductId);
                subtotal += p.Price * s.Quantity;
                htmlBodyString += "<tr>" +
                        "<td>" + p.Name + "</td>" +
                        "<td>" + s.Quantity + "</td>" +
                        "<td>" + p.Price * s.Quantity + "</td>" +
                        "</tr>";
            }
            htmlBodyString += "<tr><td>Subtotal</td><td></td><td>" + subtotal + "</td></tr>";
            htmlBodyString += "<tr><td>Delivery Fee</td><td></td><td>" + orderDetail.orderList.DeliveryFee + "</td></tr>";
            htmlBodyString += "<tr><td>Total</td><td></td><td>" + (subtotal + Convert.ToDouble(orderDetail.orderList.DeliveryFee)) + "</td></tr>";
            htmlBodyString += "</table>";
            builder.HtmlBody = htmlBodyString;

            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        /// <summary>
        /// This method sends the confirmation order of regular order to the client
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public async Task SendEmailToClientRegularAsync(OrderDetail orderDetail, int orderId, List<Product> products)
        {
            double subtotal = 0;
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse("ahnjaehwan@hotmail.com")); // Change the email address for test if needed.
            email.Subject = "Thank you for ordering from Baking Bunny";
            var builder = new BodyBuilder();
            string htmlBodyString = "Hi " + orderDetail.user.Firstname + " " + orderDetail.user.Lastname + ",<br><br>" +
                "Your order # is: <strong>" + orderId + "</strong><br><br>" +
                "We've received your order and will try our best to have your order ready on time.<br><br>" +
                //"<strong>Name</strong>: " + orderDetail.user.Firstname + " " + orderDetail.user.Lastname + "<br>" +
                //"<strong>Email</strong>: " + orderDetail.user.Email + "<br>" +
                //"<strong>Phone</strong>: " + orderDetail.user.Phone + "<br>" +
                //"<strong>Delivery</strong>: " + (orderDetail.orderList.Delivery ? "Yes<br>" : "No<br>") +
                //"<strong>Address</strong>: " + orderDetail.user.Address + "<br>" +
                //"<strong>PostalCode</strong>: " + orderDetail.user.PostalCode + "<br>" +
                //"<strong>City</strong>: " + orderDetail.user.City + "<br><br>" +
                "Your order details are as follow:<br><br>" +
                "<table>" +
                "<tr><th style='text-align: left;'>Product Name</th><th style='text-align: left;'>Quantity</th><th style='text-align: left;'>Price</th></tr>";

            foreach (SaleItem s in orderDetail.saleItems)
            {
                var p = products.Find(x => x.Id == s.ProductId);
                subtotal += p.Price * s.Quantity;
                htmlBodyString += "<tr>" +
                        "<td>" + p.Name + "</td>" +
                        "<td>" + s.Quantity + "</td>" +
                        "<td>" + p.Price * s.Quantity + "</td>" +
                        "</tr>";
            }
            htmlBodyString += "<tr><td>Subtotal</td><td></td><td>" + subtotal + "</td></tr>";
            htmlBodyString += "<tr><td>Delivery Fee</td><td></td><td>" + orderDetail.orderList.DeliveryFee + "</td></tr>";
            htmlBodyString += "<tr><td>Total</td><td></td><td>" + (subtotal + Convert.ToDouble(orderDetail.orderList.DeliveryFee)) + "</td></tr>";
            htmlBodyString += "</table>";
            htmlBodyString += "<br><br>";
            if (orderDetail.orderList.Delivery)
                htmlBodyString += "Your order will be delivered on " + orderDetail.orderList.PickupDeliveryDate + "<br><br>";
            else
                htmlBodyString += "Your order will be ready to be picked up on " + orderDetail.orderList.PickupDeliveryDate + "<br><br>";
            htmlBodyString += "Please feel free to reach us at bakingbunny.yyc@gmail.com.<br><br>";
            htmlBodyString += "Best regards,<br>";
            htmlBodyString += "Baking Bunny";

            builder.HtmlBody = htmlBodyString;

            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
