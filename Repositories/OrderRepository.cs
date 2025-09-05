using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloApi.Models;
using HelloApi.Repositories;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;


        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .OrderBy(o => o.Id)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
         
            return await _context.Orders.FindAsync(id);
        }


        public async Task<Order?> GetOrderWithPersonByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Item)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithPersonAsync()
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Item)
                .OrderBy(o => o.Id)
                .ToListAsync();
        }


        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            var existing = await _context.Orders.FindAsync(order.Id);
            if (existing is null) return null;

            existing.PersonId = order.PersonId;
            existing.Number = order.Number;
            existing.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity is null) return false;

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
