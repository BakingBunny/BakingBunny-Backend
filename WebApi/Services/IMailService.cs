using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IMailService
    {
        Task SendInternalEmailRegularAsync(OrderDetail orderDetail);
        Task SendEmailToClientRegularAsync(OrderDetail orderDetail);
        Task SendInternalEmailCustomAsync(CustomOrder customOrder);
        Task SendEmailToClientCustomAsync(CustomOrder customOrder);
    }
}
