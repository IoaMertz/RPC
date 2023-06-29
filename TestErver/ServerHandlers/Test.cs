using MessageBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.ServerMessages;

namespace TestServer.ServerHandlers
{
    public class Test : IMessageHandler<ClientRequestMessage>
    {
        public Task Handle(ClientRequestMessage message)
        {
            Console.WriteLine("kati");
            return Task.CompletedTask;
        }
    }
}
