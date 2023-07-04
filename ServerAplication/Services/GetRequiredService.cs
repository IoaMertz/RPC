using Microsoft.Extensions.DependencyInjection;
using ServerAplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAplication.Services
{
    public class GetRequiredService
    {
        private readonly IServiceProvider _serviceProvider;
        public GetRequiredService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public ICalculation GetService(string serviceName)
        {
            var service = (ICalculation)_serviceProvider.GetServices(typeof(ICalculation)).Where(se => se.GetType().Name == serviceName).FirstOrDefault();


            return (ICalculation)_serviceProvider.GetServices(typeof(ICalculation)).Where(se => se.GetType().Name == serviceName).FirstOrDefault();

        }


    }
}
