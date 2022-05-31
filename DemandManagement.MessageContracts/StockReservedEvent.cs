using System;
using System.Collections.Generic;
using System.Text;

namespace DemandManagement.MessageContracts
{
    public class StockReservedEvent : IEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
