using MessageBroker.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.ServerMessages
{
    public class ClientRequestMessage : Message
    {
        public int Number { get; set; }
        public ClientRequestMessage() 
        {
        }
    }
}
