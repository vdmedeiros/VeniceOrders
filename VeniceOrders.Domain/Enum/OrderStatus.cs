using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniceOrders.Domain.Enum
{
    public enum OrderStatus
    {
        Pending,
        Created,
        Paid,
        Shipped,
        Completed,
        Cancelled
    }
}
