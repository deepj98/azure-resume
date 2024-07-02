using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.IO;
using System.Threading.Tasks;






namespace Company.Function
{
    // public class GetResumeCounter
    // {

    //     private readonly ILogger<GetResumeCounter> _logger;

    //     public GetResumeCounter(ILogger<GetResumeCounter> logger)
    //     {
    //         _logger = logger;
    //     }

    //     [Function("GetResumeCounter")]
        
    //     public static async  Task<HttpResponseMessage> Run(
    //         [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
    //         [CosmosDBInput(databaseName:"AzureResume", containerName:"Counter", 
    //         Connection ="AzureResumeConnectionString",Id = "1" , PartitionKey = "1")] Counter counterResponse,
            
    //         // Counter counter,
    //         // [CosmosDBOutput(databaseName:"AzureResume", containerName:"Counter", Connection ="AzureResumeConnectionString", PartitionKey = "1")] out Counter updatedCounter,

    //   ILogger logger)
    //     {
    //         logger.LogInformation("C# HTTP trigger function processed a request.");
    //         //  updatedCounter = counter;
    //         // updatedCounter.Count += 1;



    //          Counter updatedCounter = counterResponse;
             
            
    //         updatedCounter.Count += 1;

    //          CosmosClient client = new("AzureResumeConnectionString");
    //         Container container = client.GetContainer("AzureResume", "Counter");
    //         await container.ReplaceItemAsync(updatedCounter.Count, updatedCounter.Id);



         

    //         var jsonToReturn = JsonConvert.SerializeObject(updatedCounter);

    //         return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
    //         {
    //             Content = new StringContent(jsonToReturn, Encoding.UTF8 , "application/json")
    //         };
    //     }
    // }
 public class GetResumeCounter
    {

        private readonly ILogger<GetResumeCounter> _logger;

        public GetResumeCounter(ILogger<GetResumeCounter> logger)
        {
            _logger = logger;
        }

        [Function("GetResumeCounter")]
        public  async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            Counter counter) // Change parameter type back to Counter
            // ILogger logger)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            counter.Count += 1; // Update the counter directly

            CosmosClient client = new CosmosClient("AzureResumeConnectionString"); // Replace with your connection string
            Container container = client.GetContainer("AzureResume", "Counter");

            // Update Cosmos DB using Cosmos SDK (consider error handling)
            await container.ReplaceItemAsync(counter, counter.Id); // No partition key needed here (modify if needed)

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }

  
}



//     // Counter updatedCounter = counter;
//             // updatedCounter.Count += 1;

//             //  CosmosClient client = new CosmosClient("AzureResumeConnectionString");
//             // Container container = client.GetContainer("AzureResume", "Counter");
//             // await container.ReplaceItemAsync(updatedCounter, updatedCounter.Id, updatedCounter.PartitionKey);

