
using HelloApi.Models;
using HelloApi.Models.DTOs;
using HelloApi.Repositories;

namespace HelloApi.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;

        public OrderDetailService(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderDetailReadDto> AddOrderDetailDtoAsync(OrderDetailCreateDto dto)
        {
            var toSave = new OrderDetail
            {
                OrderId = dto.OrderId,
                ItemId = dto.ItemId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                Total = dto.Total,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.AddOrderDetailAsync(toSave);
            var withRefs = await _repository.GetOrderDetailWithRefsByIdAsync(created.Id) ?? created;

            return MapToOrderDetailReadDto(withRefs);
        }

        public async Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailDtoAsync()
        {
            var list = await _repository.GetAllOrderDetailsWithRefsAsync();
            return list.Select(MapToOrderDetailReadDto);
        }

        public async Task<OrderDetailReadDto?> GetOrderDetailDtoById(int id)
        {
            var e = await _repository.GetOrderDetailWithRefsByIdAsync(id);
            return e is null ? null : MapToOrderDetailReadDto(e);
        }

        public Task<bool> DeleteOrderDetailDtoAsync(int id) =>
            _repository.DeleteOrderDetailAsync(id);

        private static OrderDetailReadDto MapToOrderDetailReadDto(OrderDetail d) => new()
        {
            Id = d.Id,
            Quantity = d.Quantity,
            Price = d.Price,
            Total = d.Total,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            UpdatedBy = d.UpdatedBy,
            UpdateAt = d.UpdateAt,
            Order = d.Order == null ? null : new MiniOrderDto
            {
                Id = d.Order.Id,
                Number = d.Order.Number
            },
            Item = d.Item == null ? null : new MiniItemDto
            {
                Id = d.Item.Id,
                Name = d.Item.Name
            }
        };
    }
}
