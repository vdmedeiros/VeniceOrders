using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Application.DTOs;
using VeniceOrders.Application.Interfaces;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _itemRepo;
        private readonly IRabbitMQProducer _orderPublisher;
        public OrderService(IOrderRepository orderRepo, IOrderItemRepository itemRepo, IRabbitMQProducer orderPublisher)
        {
            _orderRepo = orderRepo;
            _itemRepo = itemRepo;
            _orderPublisher = orderPublisher;
        }

        public async Task<Order> CreateOrderAsync(Order order, List<OrderItem> items)
        {
            await _orderRepo.AddAsync(order);
            await _itemRepo.AddOrderItemsAsync(items);
            await _orderRepo.SaveChangesAsync();

            var orderDto = await GetOrderByIdAsync(order.Id);
            _orderPublisher.PublishOrder(orderDto);

            return order;
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null) return (null);

            var items = await _itemRepo.GetByOrderIdAsync(orderId);

            var orderDto = new OrderDto() 
            { Id = order.Id, 
              ClienteId = order.ClienteId,
              Data = order.Data,
              Status = order.Status,
              Items = items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            return orderDto;
        }
    }
}
