using HelloApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloApi.Repositories
{
    public interface IOrderRepository
    {

        Task<Order> AddOrderAsync(Order order);

       
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderWithPersonByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersWithPersonAsync();

        Task<Order?> UpdateOrderAsync(Order order);

        Task<bool> DeleteOrderAsync(int id);
    }
}
