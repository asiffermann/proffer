namespace Proffer.Events.AzureStorage.Internal
{
    using System.Threading.Tasks;
    using Azure.Storage.Queues;

    /// <summary>
    /// Some queue storage extensions methods
    /// </summary>
    public static class QueueStorageExtensions
    {
        /// <summary>
        /// Create the a
        /// </summary>
        /// <param name="queueService">The queue service.</param>
        /// <param name="queueName">Name of the queue.</param>
        public async static Task EnsureQueueIsCreatedAsync(this QueueServiceClient queueService, string queueName)
        {
            await queueService.GetQueueClient(queueName)
                .CreateIfNotExistsAsync();
        }
    }
}
