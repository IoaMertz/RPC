using MessageBroker.Interfaces;
using RPCMessageBrokerClient;
using TestClient.ClientHandlers;
using TestClient.ClientMessages;

namespace TestClient
{
    internal class Program
    {
        public static IMessageBroker broker;
        static void Main(string[] args)
        {
            broker = new RbMqMessageBroker();

            var serverReplyQueue = broker.DeclareQueue("serverReplyQueue");

            broker.Subscribe<ServerReplyMessage, ServerReplyMessageHandler>(serverReplyQueue,true);

            var requestMessage = new ClientRequestMessage(2);

            broker.Publish(requestMessage,"DoubleNumber",serverReplyQueue);
            
        }
    }
}