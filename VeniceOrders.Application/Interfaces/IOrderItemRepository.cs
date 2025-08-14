using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Application.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddOrderItemsAsync(IEnumerable<OrderItem> items);
        Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId);
    }
}
