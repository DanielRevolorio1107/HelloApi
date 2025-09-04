using System.Collections.Generic;
using System.Threading.Tasks;
using HelloApi.Models;

namespace HelloApi.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsWithRefsAsync();
        Task<OrderDetail?> GetOrderDetailWithRefsByIdAsync(int id);
        Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
