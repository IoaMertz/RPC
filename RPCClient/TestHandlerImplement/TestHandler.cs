using MessageBroker.Interfaces;
using MessageBroker.testMessageImplementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TestHandlerImplement
{
    public class TestHandler : IMessageHandler<TestMessageImple>
    {
        public Task Handle(TestMessageImple message)
        {
            message.message += "inside TestHandler handle method";
            return Task.CompletedTask;
        }
    }
}
