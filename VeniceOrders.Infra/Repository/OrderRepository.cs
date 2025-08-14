using Microsoft.EntityFrameworkCore;
using VeniceOrders.Application.Interfaces;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Infra.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
            => await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public async Task AddAsync(Order order)
            => await _context.Orders.AddAsync(order);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
