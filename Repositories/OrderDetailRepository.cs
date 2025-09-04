using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloApi.Models;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext _context;
        public OrderDetailRepository(AppDbContext context) => _context = context;

        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails
                .OrderBy(d => d.Id)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsWithRefsAsync()
        {
            return await _context.OrderDetails
                .Include(d => d.Order)
                .Include(d => d.Item)
                .OrderBy(d => d.Id)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailWithRefsByIdAsync(int id)
        {
            return await _context.OrderDetails
                .Include(d => d.Order)
                .Include(d => d.Item)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            var existing = await _context.OrderDetails.FindAsync(orderDetail.Id);
            if (existing is null) return null;

            existing.ItemId = orderDetail.ItemId;
            existing.Quantity = orderDetail.Quantity;
            existing.Price = orderDetail.Price;
            existing.Total = orderDetail.Total;
            existing.UpdateAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var entity = await _context.OrderDetails.FindAsync(id);
            if (entity is null) return false;

            _context.OrderDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
