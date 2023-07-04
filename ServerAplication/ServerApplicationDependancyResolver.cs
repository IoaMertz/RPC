using MediatR;
using MessageBrokerDomain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ServerAplication.Interfaces;
using ServerAplication.MessageHandlers;
using ServerAplication.Services;
using ServerDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication
{
    public static class ServerApplicationDependancyResolver
    {

        public static IServiceCollection ServerApplicationRegisterServices(this IServiceCollection services)
        {

            services.AddTransient<IReplyMessageHandler<CalculationRequestMessage>, CalculationRequestMessageHandler>();

            services.AddTransient<ICalculation, AddService>();
            services.AddTransient<ICalculation,SubstractService>();
            services.AddTransient<ICalculation, MultiplyService>();

            services.AddTransient<GetRequiredService>();

            return services;
        }
    }
}
