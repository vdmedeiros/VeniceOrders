using VeniceOrders.Domain.Enum;

namespace VeniceOrders.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public DateTime Data { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid clienteId, DateTime data)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            Data = data;
            Status = OrderStatus.Pending;
        }

    }
}
