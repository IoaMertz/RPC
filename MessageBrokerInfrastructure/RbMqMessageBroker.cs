
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
using MessageBrokerDomain.Interfaces;
using RabbitMQ.Client;
using MessageBrokerDomain.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.DependencyInjection;
using MessageBrokerDomain.Commands;
using MediatR;

namespace MessageBrokerInfrastructure
{
    public class RbMqMessageBroker : IMessageBroker
    {

        //private readonly List<string> correlationIdList = new List<string>();
        private readonly ConcurrentDictionary<string,
                TaskCompletionSource<string>> callbackMapper = new();

        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;

        public RbMqMessageBroker(IServiceProvider serviceProvider, IMediator mediator)
        {
            _serviceProvider = serviceProvider;
            _mediator = mediator;
        }
        public string DeclareQueue(string QueueName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            return channel.QueueDeclare(QueueName, false, false, false, null);

        }

        public Task SendCommand<T>(T command) where T : Command
        {
            // command : message , message:IRequest<bool>, _mediator.Send() expects a IRequest<bool>
            return _mediator.Send(command);
        }

        //if we publish a message we want to add a corellationId.
        //if we publish a reply we want to take the arleady existing correlationId
        public Task<string> Publish(Message message,
            string queue,
            string? replyQueue = null,
            string? correlationId = null)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    IBasicProperties props = channel.CreateBasicProperties();

                    var tcs = new TaskCompletionSource<string>();

                    if (correlationId == null)
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

        //overload when we need to reply
        public void Subscribe<T, TH>(string subscribingQueue,
               bool correlationIdCheck = false) where T : Message
            where TH : IMessageHandler<T>
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var consumer = new AsyncEventingBasicConsumer(channel);

            var handlerType = typeof(TH);

            //var HandlerInstance = Activator.CreateInstance(HandlerType);

            var handlerInstance = _serviceProvider.GetRequiredService(handlerType);

            var handlerConcreteType = typeof(IMessageHandler<>).MakeGenericType(typeof(T));

            
            consumer.Received += async (model, ea) =>
            {
                
                // if we want to check for correlation and we cant find the Id then exit the method
                if (correlationIdCheck && !callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                {
                    return;
                }

                var messageString = Encoding.UTF8.GetString(ea.Body.ToArray());

                var messageObject = JsonConvert.DeserializeObject<T>(messageString);

                await (Task)handlerConcreteType.GetMethod("Handle").Invoke(handlerInstance, new object[] { messageObject });

            };

            channel.BasicConsume(
                consumer,
                subscribingQueue,
                autoAck: true
                );
        }


        public void SubscribeReply<T, TH>(string subscribingQueue,
              bool correlationIdCheck = false) where T : Message
           where TH : IReplyMessageHandler<T>
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var consumer = new AsyncEventingBasicConsumer(channel);

            var handlerType = typeof(TH);

            //var HandlerInstance = Activator.CreateInstance(HandlerType);
            var handlerInstance = _serviceProvider.GetRequiredService(handlerType);

            var handlerConcreteType = typeof(IReplyMessageHandler<>).MakeGenericType(typeof(T));


            consumer.Received += async (model, ea) =>
            {
                
                // if we want to check for correlation and we cant find the Id then exit the method
                if (correlationIdCheck && !callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                {
                    return;
                }

                var messageString = Encoding.UTF8.GetString(ea.Body.ToArray());

                var messageObject = JsonConvert.DeserializeObject<T>(messageString);

                var replyQueue = ea.BasicProperties.ReplyTo;

                await (Task)handlerConcreteType.GetMethod("Handle").Invoke(handlerInstance, new object[] {
                    messageObject, ea.BasicProperties.ReplyTo,ea.BasicProperties.CorrelationId });
            };

            
            channel.BasicConsume(
                consumer,
                subscribingQueue,
                autoAck: true
                );
        }
    }
}
