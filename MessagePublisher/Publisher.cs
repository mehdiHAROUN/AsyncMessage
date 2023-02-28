using Azure.Messaging.ServiceBus;

namespace MessagePublisher
{
    public class Publisher
    {
        private const string serviceBusConnectionString = "Endpoint=sb://sezame.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=h2eNJDlmqGKJgw4BlAhw4U5FIKW9mTHp8+ASbDtg0+A=";
        private const string queueName = "foundation-object";
        private const int numOfMessages = 3;
        static ServiceBusClient client = default!;
        static ServiceBusSender sender = default!;

        public async Task PublishMessage()
        {
            client = new ServiceBusClient(serviceBusConnectionString);
            sender = client.CreateSender(queueName);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            for (int i = 1; i <= numOfMessages; i++)
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }
            try
            {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
