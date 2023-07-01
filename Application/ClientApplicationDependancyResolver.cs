using Application.CommandHandlers;
using Application.Commands;
using MediatR;
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
            services.AddScoped<IRequestHandler<CalculationRequestCommand, bool>, CalculationRequestCommandHandler>();
            return services;
        }
    }
}
