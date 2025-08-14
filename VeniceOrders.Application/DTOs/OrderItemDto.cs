﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniceOrders.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Product { get; set; }
        public int Quantity { get;  set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;
    }
}
