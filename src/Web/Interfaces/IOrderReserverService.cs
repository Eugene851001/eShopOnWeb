using DeliveryOrderProcessorFunction.DTO;
using Microsoft.eShopWeb.Web.Services;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IOrderReserverService
{
    Task ReserverOrder(OrderDTO order);
}
