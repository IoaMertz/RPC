using MessageBroker.Messages;
using System.Buffers;

namespace MessageBroker.Interfaces
{
    public interface IMessageBroker
    {
        string DeclareQueue(string QueueName);
        Task<string> Publish(Message message, string queue, string replyQueue, string? correlationId = null);
        void Subscribe<T,TH>(string queue,bool correlationIdCheck = false)
            where T : Message 
            where TH : IMessageHandler<T>;
    }
}