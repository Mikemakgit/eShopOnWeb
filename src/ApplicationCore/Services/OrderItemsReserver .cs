using System;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;


namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class OrderItemsReserver : IOrderItemsReserver
{
   private readonly string _learnshopbusConnectionString;
   const string QueueName = "learnshopqueue";


    public OrderItemsReserver(IConfiguration configuration)
    {
        _learnshopbusConnectionString = configuration["learnshopbusconnectionstring"];
    }


    public async Task SendOrderToOrderItemsReserver(Order order)
    {
        await using var client = new ServiceBusClient(_learnshopbusConnectionString);

        await using ServiceBusSender sender = client.CreateSender(QueueName);
       
        try
        {
            var messageBody = JsonConvert.SerializeObject(order);
            var message = new ServiceBusMessage(messageBody);
            await sender.SendMessageAsync(message);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
        }
        finally
        {
           await sender.DisposeAsync();
           await client.DisposeAsync();
        }
    }


    //public static async Task Run(string myQueueItem)
    //{

    //    var client = new ServiceBusClient("ServiceBusConnectionString");

    //    var processorOptions = new ServiceBusProcessorOptions
    //    {
    //        MaxConcurrentCalls = 1,
    //        AutoCompleteMessages = true
    //    };

    //    await using ServiceBusProcessor processor = client.CreateProcessor(QueueName, processorOptions);

    //    processor.ProcessMessageAsync += MessageHandler;
    //    processor.ProcessErrorAsync += ErrorHandler;


    //    await processor.StartProcessingAsync();

    //    Console.Read();

    //    await processor.CloseAsync();


    //}

    //static async Task MessageHandler(ProcessMessageEventArgs args)
    //{
    //    string body = args.Message.Body.ToString();
    //    Console.WriteLine($"Received: {body}");

    //    await args.CompleteMessageAsync(args.Message);
    //}

    //static Task ErrorHandler(ProcessErrorEventArgs args)
    //{
    //    Console.WriteLine(args.Exception.ToString());
    //    return Task.CompletedTask;
    //}
}

