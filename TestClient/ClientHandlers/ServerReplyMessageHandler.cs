using MessageBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClient.ClientMessages;

namespace TestClient.ClientHandlers
{
    public class ServerReplyMessageHandler : IMessageHandler<ServerReplyMessage>
    {
        public Task Handle(ServerReplyMessage message)
        {
            Console.WriteLine("WE ARE INSIDE ClientReplyMessageHandler Handle method\n");

            Console.WriteLine($"I handle the message {message.MessageType}," +
                $" GetType() from this. {this.GetType().Name}\n");

            Console.WriteLine($"I got the message with INT : {message.Number}\n");

            return Task.CompletedTask;

        }
    }
}
