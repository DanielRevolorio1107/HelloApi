using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IOrderDetailService
    {
        Task<OrderDetailReadDto> AddOrderDetailDtoAsync(OrderDetailCreateDto orderDetail);
        Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailDtoAsync();
        Task<OrderDetailReadDto?> GetOrderDetailDtoById(int id);
        Task<bool> DeleteOrderDetailDtoAsync(int id);
    }
}
