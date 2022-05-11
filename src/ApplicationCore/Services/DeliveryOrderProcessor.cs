using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class DeliveryOrderProcessor : IDeliveryOrderProcessor
{
    public async Task SendOrderToDeliveryOrderProcessor(Order order)
    {
        var url = "https://deliveryorderprocessorfunction.azurewebsites.net/api/DeliveryOrderProcessorFunction?";

        var data = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");

        await new HttpClient().PostAsync(url, data);
    }
}

