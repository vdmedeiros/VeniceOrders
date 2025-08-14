using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task AddAsync(Order order);
        Task SaveChangesAsync();
    }
}
