using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VeniceOrders.Domain.Entities
{
    public class OrderItem
    {
        [BsonRepresentation(BsonType.String)]
        public Guid OrderId { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Total => Quantity * Price;

        public OrderItem(Guid orderId, string product, int quantity, decimal price)
        {
            OrderId = orderId;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
