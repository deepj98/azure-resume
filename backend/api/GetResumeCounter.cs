using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Company.Function
{
    public class GetResumeCounter
    {
        private readonly ILogger<GetResumeCounter> _logger;

        public GetResumeCounter(ILogger<GetResumeCounter> logger)
        {
            _logger = logger;
        }

        [Function("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [CosmosDBInput(databaseName:"AzureResume", containerName:"Counter", Connection ="AzureResumeConnectionString",Id = "1" , PartitionKey = "1")] Counter counter,
            // [CosmosDBOutput(databaseName:"AzureResume", containerName:"Counter", Connection ="AzureResumeConnectionString", PartitionKey = "1")] out Counter updatedCounter,
    
      ILogger logger)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            // updatedCounter = counter;
            // updatedCounter.Count += 1;

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8 , "application/json")
            };
        }
    }
}
