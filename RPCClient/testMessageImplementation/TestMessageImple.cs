using MessageBroker.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.testMessageImplementation
{
    public class TestMessageImple :Message
    {
        public string message { get; set; }
        public TestMessageImple()
        {
            message = $"{this.MessageType} + from message ctor + ";
        }
    }
}
