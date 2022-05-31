using DemandManagement.MessageContracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace StockConsole
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine($"Gelen mesaj : {context.Message.OrderId} " + DateTime.Now);

            //throw new Exception("Hata oluştu şimdi");

            ISendEndpoint sendEndpoint = await context.GetSendEndpoint(new Uri($"queue:{RabbitMqConsts.Payment_StockReservedEventQueue}"));
            StockReservedEvent stockReservedEvent = new StockReservedEvent()
            {
                BuyerId = context.Message.BuyerId,
                OrderId = context.Message.OrderId,
                OrderItems = context.Message.OrderItems,
                TotalPrice = context.Message.TotalPrice
            };
            await sendEndpoint.Send(stockReservedEvent);

            Console.WriteLine($"StockReservedEvent : {context.Message.OrderId}");
        }
    }

    public class OrderCreatedEventFaultConsumer : IConsumer<Fault<OrderCreatedEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<OrderCreatedEvent>> context)
        {
            Console.WriteLine("Hata oluştu: " + context.Message.Message.BuyerId);



        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedEventConsumer>();//.Endpoint(x => x.Name= RabbitMqConsts.Stock_OrderCreatedEventQueue);

                x.AddConsumer<OrderCreatedEventFaultConsumer>();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.UseMessageRetry(r => r.Immediate(3));
                    cfg.UseMessageRetry(r => r.Intervals(3000, 5000, 10000));

                    cfg.Host(RabbitMqConsts.RabbitMqUri, configurator =>
                    {
                        configurator.Username(RabbitMqConsts.UserName);
                        configurator.Password(RabbitMqConsts.Password);
                        //configurator.UseCluster(c =>
                        //{
                        //    c.Node("localhost:5673");
                        //    c.Node("localhost:5674");
                        //    c.Node("localhost:5675");
                        //});
                    });
                    cfg.ConfigureEndpoints(context);
                });



            });

            var serviceProvider = services.BuildServiceProvider();
            var bus = serviceProvider.GetRequiredService<IBusControl>();



            bus.StartAsync();
            Console.ReadLine();
            bus.StopAsync();
        }
    }
}
