using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniceOrders.Application.UseCases.CreateOrder
{
    public record CreateOrderCommand(Guid ClienteId, List<CreateOrderItem> Items);

    public record CreateOrderItem(string Product, int Quantity, decimal Price);
}
