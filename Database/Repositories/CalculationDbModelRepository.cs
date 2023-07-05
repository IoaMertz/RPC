using Database.DbModels;
using Database.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class CalculationDbModelRepository :IRepository<CalculationDbModel>
    {
        private readonly CosmosClient _client;

        private readonly Container _container;


        public CalculationDbModelRepository(CosmosClient cosmosClient)
        {
            _client = cosmosClient;

            _container = _client.GetContainer("Giannis-Project", "Calculations");
        }


        public async Task<CalculationDbModel> AddAsync(CalculationDbModel calculationDbModel)
        {

            var response = await _container.CreateItemAsync(calculationDbModel);

            return calculationDbModel;

        }



        public async Task<IEnumerable<CalculationDbModel>> GetAllAsync()
        {
            //var items = _container.GetItemLinqQueryable<CalculationDbModel>().ToList();
            //return items;
            var query = _container.GetItemLinqQueryable<CalculationDbModel>();
            var iterator = query.ToFeedIterator<CalculationDbModel>();
            var items = new List<CalculationDbModel>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                items.AddRange(response);
            }

            return items;

        }






    }
}
