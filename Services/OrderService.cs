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
                CreatedAt = DateTime.UtcNow,
            };

            var created = await _repository.AddOrderAsync(toSave);

   
            var saved = await _repository.GetOrderWithPersonByIdAsync(created.Id) ?? created;

            return MapToOrderReadDto(saved);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrderDtoAsync()
        {
       
            var entities = await _repository.GetAllOrdersWithPersonAsync();
            return entities.Select(MapToOrderReadDto);
        }

        public async Task<OrderReadDto?> GetOrderDtoById(int id)
        {
           
            var entity = await _repository.GetOrderWithPersonByIdAsync(id);
            return entity is null ? null : MapToOrderReadDto(entity);
        }

        public async Task<OrderReadDto?> UpdateOrderDtoAsync(int id, OrderUpdateDto order)
        {
           
            var existing = await _repository.GetOrderByIdAsync(id);
            if (existing is null) return null;

            existing.PersonId = order.PersonId;
            existing.Number = order.Number;

           
            var updated = await _repository.UpdateOrderAsync(existing);
            if (updated is null) return null;

            
            var withPerson = await _repository.GetOrderWithPersonByIdAsync(updated.Id) ?? updated;

            return MapToOrderReadDto(withPerson);
        }

        public Task<bool> DeleteOrderDtoAsync(int id) => _repository.DeleteOrderAsync(id);

        private static OrderReadDto MapToOrderReadDto(Order order) => new()
        {
            Id = order.Id,
            Number = order.Number,
            Person = order.Person == null ? null : new PersonReadDto
            {
                Id = order.Person.Id,
                FirstName = order.Person.FirstName,
                LastName = order.Person.LastName
            }
        };
    }
}
