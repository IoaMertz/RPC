using MessageBroker.Interfaces;
using RPCMessageBrokerClient;
using TestServer.ServerMessages;
using TestServer.ServerHandlers;

namespace TestServer
{
    internal class Program
    {
        public static IMessageBroker broker;
        static void Main(string[] args)
        {
            broker = new RbMqMessageBroker();

            broker.DeclareQueue("DoubleNumber");

            broker.Subscribe<ClientRequestMessage, ClientRequestMessageHandler>("DoubleNumber");

            broker.Publish
        }
    }
}