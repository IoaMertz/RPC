using Database.DbModels;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class LogInService
    {
        private readonly CosmosClient _client;

        private readonly Container _container;


        public LogInService(CosmosClient cosmosClient)
        {
            _client = cosmosClient;

            _container = _client.GetContainer("Giannis-Project", "Users");
        }

        public async Task<string?> GetUsersId(string userName, string passWord)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<UserDbModel>().Where(user => user.UserName==userName && user.Password==passWord);

                var iterator = query.ToFeedIterator<UserDbModel>();

                var response = await iterator.ReadNextAsync();
                var kati = response.FirstOrDefault().Id;

                return kati;

            }catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string?> ValidateUser(string userId)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<UserDbModel>().Where(user => user.Id == userId);

                var iterator = query.ToFeedIterator<UserDbModel>();

                var response = await iterator.ReadNextAsync();

                return response.FirstOrDefault().Id;
            }
            catch (Exception)
            {

                return null;
            }
        }
        
    }
}
