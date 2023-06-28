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


        public Task<string> Publish<T>(T message, string queue, string replyQueue) where T : Message
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

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

                    var body = Encoding.UTF8.GetBytes(bodyContent);

                    channel.BasicPublish(
                        string.Empty,
                        queueName,
                        props,
                        body
                        );

                    var tcs = new TaskCompletionSource<string>();

                    callbackMapper.TryAdd(correlationId, tcs);

                    return tcs.Task;
                }
            }
        }

        public void Subscribe<TH>(string subscribingQueue,bool correlationIdCheck = false)
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
