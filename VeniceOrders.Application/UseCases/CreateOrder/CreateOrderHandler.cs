using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Application.Interfaces;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Application.UseCases.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderService _orderService;

        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Guid> Handle(CreateOrderCommand command)
        {
            var order = new Order(command.ClienteId, DateTime.UtcNow);
            
            var items = new List<OrderItem>();
            foreach (var item in command.Items)
            {
                items.Add(new OrderItem(order.Id, item.Product, item.Quantity, item.Price));
            }

            await _orderService.CreateOrderAsync(order, items);

            return order.Id;
        }
    }
}
