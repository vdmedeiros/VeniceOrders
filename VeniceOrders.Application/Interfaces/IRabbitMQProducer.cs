using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Application.DTOs;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Application.Interfaces
{
    public interface IRabbitMQProducer
    {
        void PublishOrder<T>(T message);
    }
}
