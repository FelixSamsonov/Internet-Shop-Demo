using InternetShopAspNetCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopAspNetCoreMvc.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(int id);

        Task<IEnumerable<Order>> GetUserOrdersWithDetailsAsync(int id);

        Task<Order?> GetOrderWithDetailsAsync(int id);

        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task ConfirmOrderAsync(int userId);
    }
}
