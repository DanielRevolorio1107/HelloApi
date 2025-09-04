using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IItemService
    {
        Task<ItemreadDto> CreateItemAsync(ItemCreateDto item);
        Task<IEnumerable<ItemreadDto>> GetAllItemsAsync();
        Task<ItemreadDto?> GetItemByIdAsync(int id);
        Task<ItemreadDto?> UpdateItemAsync(int id, ItemUpdateDto item);
        Task<bool> DeleteItemAsync(int id);
    }
}