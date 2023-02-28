using System;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionMessageReader
{
    public class MessageReader
    {
        [FunctionName("MessageReader")]
        public void Run([ServiceBusTrigger("foundation-object", Connection = "FunctionReader")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}