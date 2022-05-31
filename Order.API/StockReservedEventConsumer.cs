using DemandManagement.MessageContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            System.Diagnostics.Debug.WriteLine("StockReservedEvent worked");

            throw new Exception("efewf");
            
        }
    }

    public class StockReservedConsumerDefinition : ConsumerDefinition<StockReservedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<StockReservedEventConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(r =>
            {
                r.Immediate(2);
                r.Ignore<ArgumentNullException>();

            });
        }
    }

    public class StockReservedEventFaultConsumer : IConsumer<Fault>
    {
        public async Task Consume(ConsumeContext<Fault> context)
        {
            System.Diagnostics.Debug.WriteLine("Hata oluştu: " + context.Message);

        }
    }
}
