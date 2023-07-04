using Database.DbModels;
using Database.Interfaces;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class  DataBaseDependancyResolver
    {
        

            public static IServiceCollection DatabaseRegisterServices(this IServiceCollection services)
            {

                services.AddTransient<IRepository<CalculationDbModel>, CalculationDbModelRepository>();

                return services;
            }
        

    }
}
