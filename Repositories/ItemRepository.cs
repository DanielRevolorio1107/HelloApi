
using HelloApi.Models;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;
using HelloApi.Repositories;

namespace HelloApi.Repositories;


public class ItemRepository(AppDbContext context) : IItemRepository


{
    private readonly AppDbContext _context = context;

    public async Task<Item> AddItemAsync(Item item)
    {
       
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return item;

    }
    public async Task<IEnumerable<Item>> GetAllItemAsync()
    {
        return await _context.Items.OrderBy(m => m.Id).ToListAsync();
    }

    public async Task<Item?> GetItemByIdAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item?> UpdateItemAsync(Item item)
    {
        var existing = await _context.Items.FindAsync(item.Id);
        if (existing == null) return null;
        existing.Name = item.Name;
        existing.Price = item.Price;
        existing.UpdateAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        var entity = await _context.Items.FindAsync(id);
        if (entity == null) return false;
        _context.Items.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
