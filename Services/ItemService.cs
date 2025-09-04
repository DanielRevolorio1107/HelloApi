using HelloApi.Models;
using HelloApi.Models.DTOs;
using HelloApi.Repositories;
using MessageApi.Repositories;

namespace HelloApi.Services
{
    public class ItemService(IItemRepository repository) : IItemService
    {
        private readonly IItemRepository _repository = repository;

        public async Task<ItemreadDto> CreateItemAsync(ItemCreateDto item)
        {
            var itemdto = new Item
            {
                Name = item.Name,
                Price = item.Price,
            };

            var entity = await _repository.AddItemAsync(itemdto);
            return MapToReadDto(entity);
        }

        public async Task<IEnumerable<ItemreadDto>> GetAllItemsAsync()
        {
            var entities = await _repository.GetAllItemAsync();
            return entities.Select(MapToReadDto);
        }

        public async Task<ItemreadDto?> GetItemByIdAsync(int id)
        {
            var entity = await _repository.GetItemByIdAsync(id);
            return entity == null ? null : MapToReadDto(entity);
        }

        public async Task<ItemreadDto?> UpdateItemAsync(int id, ItemUpdateDto item)
        {
            var entity = await _repository.GetItemByIdAsync(id);
            if (entity == null) return null;

            entity.Name = item.Name;
            entity.Price = item.Price;

            await _repository.UpdateItemAsync(entity);

            return new ItemreadDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
            };
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _repository.DeleteItemAsync(id);
        }

        private static ItemreadDto MapToReadDto(Item item) => new()
        {
            Id = item.Id,
           Name = item.Name,
           Price = item.Price,
        };

    }
}