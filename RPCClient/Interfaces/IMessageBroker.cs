using MessageBroker.Messages;

namespace MessageBroker.Interfaces
{
    public interface IMessageBroker
    {
        string DeclareQueue(string QueueName);
        Task<string> Publish<T>(T message, string queue, string replyQueue) where T : Message;
        void Subscribe<TH>(string queue,bool correlationIdCheck = false);
    }
}