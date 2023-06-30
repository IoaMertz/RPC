

using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class GetCalculationService
    {
        private readonly IServiceProvider _serviceProvider;

        public GetCalculationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //public ICalculationService GetService(string ServiceName)
        //{
        //    Type ServiceType = Type.GetType(ServiceName);
        //    var serviceInstance =  _serviceProvider.GetService(ServiceType);

            

            
        //}

         
    }
}
