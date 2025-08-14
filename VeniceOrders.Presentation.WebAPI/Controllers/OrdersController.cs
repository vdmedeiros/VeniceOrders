using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using VeniceOrders.Application.DTOs;
using VeniceOrders.Application.Interfaces;
using VeniceOrders.Application.UseCases.CreateOrder;

namespace VeniceOrders.Presentation.WebAPI.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderHandler _createOrderHandler;
        private readonly IOrderService _orderService;
        private readonly IDistributedCache _cache;

        public OrdersController(CreateOrderHandler createOrderHandler, IOrderService orderService, IDistributedCache cache)
        {
            _createOrderHandler = createOrderHandler;
            _orderService = orderService;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var orderId = await _createOrderHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            string cacheKey = $"order_{orderId}";

            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                var cachedOrder = JsonSerializer.Deserialize<OrderDto>(cachedData);
                return Ok(cachedOrder);
            }

            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            var serialized = JsonSerializer.Serialize(order);
            await _cache.SetStringAsync(cacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            });

            return Ok(new { order });
        }
    }
}

