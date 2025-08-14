using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Application.Interfaces;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Infra.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IMongoCollection<OrderItem> _orderItems;

        public OrderItemRepository(IMongoClient client, string dbName)
        {
            var database = client.GetDatabase(dbName);
            _orderItems = database.GetCollection<OrderItem>("orderitems");
        }

        public async Task AddOrderItemsAsync(IEnumerable<OrderItem> items)
        {
            try
            {
                await _orderItems.InsertManyAsync(items);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId) =>
         await _orderItems.Find(i => i.OrderId == orderId).ToListAsync();
    }
}
