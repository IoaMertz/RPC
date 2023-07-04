using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class CreateCosmosDatabase
    {
        
        public static async void Create(CosmosClient cosmosClient)
        {
            var database = await cosmosClient.CreateDatabaseIfNotExistsAsync("Giannis-Project");
            var userContainer = database.Database.CreateContainerIfNotExistsAsync("Users", "/Id").GetAwaiter().GetResult();
            var calculationsContainer = database.Database.CreateContainerIfNotExistsAsync("Calculations", "/CalculationId").GetAwaiter().GetResult();
            //var logContainer = database.CreateContainerIfNotExistsAsync("Logs", "/id").GetAwaiter().GetResult();
        }
    }


        
    
}

