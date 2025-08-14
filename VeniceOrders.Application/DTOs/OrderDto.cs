using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Domain.Enum;

namespace VeniceOrders.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime Data { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
