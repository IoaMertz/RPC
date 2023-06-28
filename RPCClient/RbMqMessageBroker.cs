using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MessageBroker.Interfaces;
using MessageBroker.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RPCMessageBrokerClient
{
    public class RbMqMessageBroker : IMessageBroker
    {

        //private readonly List<string> correlationIdList = new List<string>();
        private readonly ConcurrentDictionary<string,
                TaskCompletionSource<string>> callbackMapper = new();
        public string DeclareQueue(string QueueName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            return channel.QueueDeclare(QueueName);
        }

        //if we publish a message we want to add a corellationId.
        //if we publish a reply we want to take the arleady existing correlationId
        public Task<string> Publish(Message message,
            string queue,
            string replyQueue,
            string? correlationId = null) 
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    IBasicProperties props = channel.CreateBasicProperties();

                    var tcs = new TaskCompletionSource<string>();

                    if(correlationId == null)
                    {
                        correlationId = Guid.NewGuid().ToString();
                        callbackMapper.TryAdd(correlationId, tcs);
                    }

                    props.CorrelationId = correlationId;

                    props.ReplyTo = replyQueue;

                    var queueName = queue;

                    var bodyContent = JsonConvert.SerializeObject(message);

                    var body = Encoding.UTF8.GetBytes(bodyContent);

                    channel.BasicPublish(
                        string.Empty,
                        queueName,
                        props,
                        body
                        );

                    return tcs.Task;
                }
            }
        }

        //if we subscribe to reply we want to check the correlationId.
        //If we subscribe to a message we do not want to check
        public void Subscribe<T,TH>(string subscribingQueue,
            bool correlationIdCheck = false) where T : Message
            where TH : IMessageHandler<T>
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new AsyncEventingBasicConsumer(channel);

                    var HandlerType = typeof(TH);

                    var handlerMethod = HandlerType.GetMethod("Handle");

                    consumer.Received += async (model, ea) =>
                    {
                        // if we want to check for correlation and we cant find the Id then exit the method
                        if (correlationIdCheck && !callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                        {
                            return;
                        }

                        var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                        await (Task)handlerMethod.Invoke(null, new object[] { message });

                    };

                    channel.BasicConsume(
                        consumer,
                        subscribingQueue,
                        autoAck: true
                        );
                }
            }
        }


        
    }
}
