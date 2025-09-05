using HelloApi.Models;
using HelloApi.Models.DTOs;
using HelloApi.Repositories;

namespace HelloApi.Services
{
    public class OrderService(Repositories.IOrderRepository repository) : IOrderService
    {
        private readonly Repositories.IOrderRepository _repository = repository;

        public async Task<OrderReadDto> AddOrderDtoAsync(OrderCreateDto order)
        {
            var toSave = new Order
            {
                PersonId = order.PersonId,
                Number = order.Number,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.AddOrderAsync(toSave);
            // Trae con relaciones para proyectar completo
            var saved = await _repository.GetOrderWithPersonByIdAsync(created.Id) ?? created;
            return MapToOrderReadDto(saved);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrderDtoAsync()
        {
            var entities = await _repository.GetAllOrdersWithPersonAsync();
            return entities.Select(MapToOrderReadDto);
        }

        public async Task<OrderReadDto?> GetOrderDtoById(int id /* OJO: tu controller usa {number:int} */)
        {
            var entity = await _repository.GetOrderWithPersonByIdAsync(id);
            return entity == null ? null : MapToOrderReadDto(entity);
        }

        public async Task<OrderReadDto?> UpdateOrderDtoAsync(int id, OrderUpdateDto dto)
        {
            var existing = await _repository.GetOrderByIdAsync(id);
            if (existing == null) return null;

            existing.PersonId = dto.PersonId;
            existing.Number = dto.Number;
            existing.UpdateAt = DateTime.UtcNow;

            var updated = await _repository.UpdateOrderAsync(existing);
            if (updated == null) return null;

            var withRefs = await _repository.GetOrderWithPersonByIdAsync(updated.Id) ?? updated;
            return MapToOrderReadDto(withRefs);
        }

        public async Task<bool> DeleteOrderDtoAsync(int id)
        {
            return await _repository.DeleteOrderAsync(id);
        }

        private static OrderReadDto MapToOrderReadDto(Order o) => new()
        {
            Id = o.Id,
            Number = o.Number,
            Person = o.Person is null ? null : new PersonReadDto
            {
                Id = o.Person.Id,
                FirstName = o.Person.FirstName,
                LastName = o.Person.LastName,
                Email = o.Person.Email ?? string.Empty
            },
            OrderDetail = (o.OrderDetails ?? Enumerable.Empty<OrderDetail>()).Select(d => new OrderDetailReadDto
            {
                Id = d.Id,
                ItemId = d.ItemId,
                ItemName = d.Item != null ? d.Item.Name : string.Empty,
                Price = d.Price,
                Quantity = d.Quantity,
                Total = d.Total
            }).ToList()
        };
    }
}
