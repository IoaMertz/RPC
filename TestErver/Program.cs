using MessageBroker.Interfaces;
using RPCMessageBrokerClient;
using TestServer.ServerMessages;
using TestServer.ServerHandlers;

namespace TestServer
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
             IMessageBroker broker = new RbMqMessageBroker();
            Console.WriteLine("hello");

            broker.DeclareQueue("DoubleNumber");



            //broker.SubscribeReply<ClientRequestMessage, ClientRequestMessageHandler>("DoubleNumber");
            Console.WriteLine("asdasdasdsad");
            broker.Subscribe<ClientRequestMessage, Test>("DoubleNumber");

            Console.ReadKey();


        }
    }
}