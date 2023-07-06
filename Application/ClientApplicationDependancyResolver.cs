using Application.CommandHandlers;
using Application.Commands;
using Application.MessageHandlers;
using Application.Messages;
using MediatR;
using MessageBrokerDomain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ClientApplicationDependancyResolver
    {
        public static IServiceCollection ClientApplicationRegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CalculationRequestCommand, CalculationResponseMessage>, CalculationRequestCommandHandler>();

            services.AddTransient<IMessageHandler<CalculationResponseMessage>, CalculationResponseMessageHandler>();

            services.AddTransient<IRequestHandler<UserValidationCommand, string>, UserValidationCommandHandler>();

            services.AddTransient<IMessageHandler<UserValidationResponseMessage>, UserValidationResponseMessageHandler>();

            return services;
        }
    }
}
