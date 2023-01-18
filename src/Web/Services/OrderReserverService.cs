using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Services;

public class OrderReserverService : IOrderReserverService
{
    private const string ServiceUrl = "https://eshopfunctionapp.azurewebsites.net/api/ItemsOrderReserver";
    private const string FunctionKey = "NbK2_lC3uAsY_DZXh0rL7ZCm_8WLcH_sMcf0ZDYmVnUAAzFu4Rdr3g==";

    public async Task ReserverOrder(OrderDTO order)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-functions-key", FunctionKey);

        string fileName = Guid.NewGuid().ToString();
        
        await client.PostAsJsonAsync($"{ServiceUrl}?name={fileName}", order);
    }
}
