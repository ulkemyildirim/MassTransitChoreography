using System;
using System.Collections.Generic;
using System.Text;

namespace DemandManagement.MessageContracts
{
    public class RabbitMqConsts
    {
        public const string RabbitMqUri = "";
        public const string UserName = "";
        public const string Password = "";
        public const string RegisterDemandServiceQueue = "registerdemand.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string ThirdPartyServiceQueue = "thirdparty.service";
        public const string Stock_OrderCreatedEventQueue = "order-created-event";
        public const string Payment_StockReservedEventQueue = "stock-reserved-event";
    }
}
