﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DemandManagement.MessageContracts
{
    public class OrderCreatedEvent : IEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }

    public class OrderItemMessage
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
