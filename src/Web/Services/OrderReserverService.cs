using DeliveryOrderProcessorFunction.DTO;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Services;

public class OrderReserverService : IOrderReserverService
{
    private const string ServiceUrl = "https://orderitemsfunction.azurewebsites.net/api/ItemsOrderReserver?";

    private readonly IConfiguration _configuration;

    public OrderReserverService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ReserverOrder(OrderDTO order)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-functions-key", _configuration["FunctionKey"]);

        string fileName = Guid.NewGuid().ToString();
        
        var response = await client.PostAsJsonAsync(ServiceUrl, order);

        Console.WriteLine(response.StatusCode);
    }
}
