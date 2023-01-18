using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using System.Web.Http;
using System.Text;

namespace OrderItemsreserverFunction
{
    public static class ItemsOrderReserver
    {
        private static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=eshopstorage42;AccountKey=QdlYfsU7HAKmFNoUnpy9djDJ9EudMZ070tisPV5cMuHKBru7qKoby3RVCYTpiHAOIR10YCl0hNbM+AStat3NoQ==;EndpointSuffix=core.windows.net";
        private static string ContainerName = "orders";

        [FunctionName("ItemsOrderReserver")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var blobServiceClient = new BlobServiceClient(ConnectionString);
                var blobContainer = blobServiceClient.GetBlobContainerClient(ContainerName);
                await blobContainer.CreateIfNotExistsAsync();

                string name = req.Query["name"];
                var blobClient = blobContainer.GetBlobClient(name);
                await blobClient.UploadAsync(req.Body);

                return new OkObjectResult($"{name} was added to a blob");
            }
            catch (Exception ex)
            {
                log.LogInformation("Logging info");
                log.LogError(ex.Message);

                return new ExceptionResult(ex, true);
            }
        }
    }
}
