using MessageBroker.Interfaces;
using MessageBroker.testMessageImplementation;
using RPCMessageBrokerClient;
using System.Reflection;

namespace MessageBroker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMessageBroker messageBroker = new RbMqMessageBroker();
        }

        public void Client(IMessageBroker messageBroker, string subscribingQueue)
        {
            //declare  replyQueue
            var replyQueueName = messageBroker.DeclareQueue("replyQueue");

            //subscribe to reply queue
            //here we need to check is the correlationId is present
            messageBroker.Subscribe<TestMessageImple>(replyQueueName);



        }

        public void Server(IMessageBroker messageBroker)
        {
            // declare messageQueue
            var messageQueue = messageBroker.DeclareQueue("MainMessageQueue");

            //subscribe to message queue
            messageBroker.Subscribe<TestMessageImple>(messageQueue);

        }
    }
}