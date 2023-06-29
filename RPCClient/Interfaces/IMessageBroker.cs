using MessageBroker.Messages;
using System.Buffers;

namespace MessageBroker.Interfaces
{
    public interface IMessageBroker
    {
        string DeclareQueue(string QueueName);
        Task<string> Publish(Message message, string queue, string replyQueue, string? correlationId = null);
        void Subscribe<T,TH>(string subscribingQueue,
              bool correlationIdCheck = false)
            where T : Message 
            where TH : IMessageHandler<T>;

        void SubscribeReply<T, TH>(string subscribingQueue,
             bool correlationIdCheck = false) where T : Message
          where TH : IReplyMessageHandler<T>;
    }
}