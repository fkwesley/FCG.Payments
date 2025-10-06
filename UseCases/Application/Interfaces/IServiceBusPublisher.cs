namespace Application.Interfaces
{
    public interface IServiceBusPublisher
    {
        Task PublishMessageAsync(string topicName, object message, IDictionary<string, object>? customProperties = null);
    }
}
