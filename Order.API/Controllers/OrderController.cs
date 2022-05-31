using DemandManagement.MessageContracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;
        readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderController(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            OrderItemMessage orderItemMessage = new OrderItemMessage()
            {
                Price = 100,
                Count = 2,
                ProductId = 3
            };

            List<OrderItemMessage> orderItemMessageList = new List<OrderItemMessage>() { orderItemMessage };


            OrderCreatedEvent orderCreatedEvent = new OrderCreatedEvent()
            {


                OrderId = 1,
                BuyerId = 1,
                TotalPrice = 100,
                OrderItems = orderItemMessageList
            };

            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqConsts.Stock_OrderCreatedEventQueue}"));
            await sendEndpoint.Send<OrderCreatedEvent>(orderCreatedEvent);


            return Ok(true);
        }

    }
}
