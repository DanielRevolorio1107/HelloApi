using HelloApi.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloApi.Services
{
    public interface IOrderService
    {
        Task<OrderReadDto> AddOrderDtoAsync(OrderCreateDto order);
        Task<IEnumerable<OrderReadDto>> GetAllOrderDtoAsync();
        Task<OrderReadDto?> GetOrderDtoById(int id);          // <-- si prefieres number, dilo y lo cambiamos
        Task<OrderReadDto?> UpdateOrderDtoAsync(int id, OrderUpdateDto order);
        Task<bool> DeleteOrderDtoAsync(int id);
    }
}
