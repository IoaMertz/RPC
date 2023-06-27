using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RPCClient
{
    public class RbMqMessageBroker
    {

        private readonly List<string> correlationIdList = new List<string>();


       public void DeclareQueue(string QueueName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
        }


        public void Publish<T>(T message, string queue, string replyQueue) where T : Message
        {
            var factory = new ConnectionFactory() { HostName = "localhost"};

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    IBasicProperties props = channel.CreateBasicProperties();

                    var correlationId = Guid.NewGuid().ToString();

                    props.CorrelationId = correlationId;

                    props.ReplyTo = replyQueue;

                    var queueName = queue;

                    var bodyContent = JsonConvert.SerializeObject(message);

                    var body =Encoding.UTF8.GetBytes(bodyContent);

                    channel.BasicPublish(
                        string.Empty,
                        queueName,
                        props,
                        body
                        );

                    correlationIdList.Add(correlationId);
                }
            }
        }

        public void Subscribe<TH>(string queue)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new AsyncEventingBasicConsumer(channel);

                    var HandlerType = typeof(TH);

                    consumer.Received += async (model, ea) =>
                    {
                         (typeof(IEventHandler<>).MakeGenericType(HandlerType)).GetMethod("Handle");
                    };

                    channel.BasicConsume(
                        consumer,
                        queue,
                        autoAck: true
                        );
                }
            }
        }
    }

   

    internal interface IEventHandler<T>
    {
    }
}
