using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.Web.Http;
using System.Text;
using Microsoft.Azure.Cosmos;
using System.Text.Json;
using System.Linq;
using System.Configuration;

namespace OrderItemsreserverFunction
{
    public static class ItemsOrderReserver
    {
        [FunctionName("ItemsOrderReserver")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                string containerName = GetEnvironmentVariable("ContainerName");
                string connectionString = GetEnvironmentVariable("StorageConnectionString");
                

                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainer = blobServiceClient.GetBlobContainerClient(containerName);
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

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
