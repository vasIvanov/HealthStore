using HealthStore.DL.Interfaces.Products;
using HealthStore.Models.Common;
using HealthStore.Models.Products;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Repositories.Products
{
    public class DietRepository : IDietRepository
    {
        private readonly IMongoCollection<Diet> _diets;
        public DietRepository(IOptions<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _diets = database.GetCollection<Diet>("Diet");
        }
        public async Task<Diet> Create(Diet diet)
        {
            await _diets.InsertOneAsync(diet);
            return diet;
        }

        public async Task Delete(int dietId)
        {
            await _diets.DeleteOneAsync(p => p.Id == dietId);
        }

        public async Task<IEnumerable<Diet>> GetAll()
        {
            var result = await _diets.FindAsync(p => true);
            return result.ToEnumerable();
        }

        public async Task<Diet> GetById(int dietId)
        {
            var result = await _diets.FindAsync(p => p.Id == dietId);
            return result.FirstOrDefault();
        }

        public async Task<Diet> GetByName(string name)
        {
            var result = await _diets.FindAsync(p => p.Name == name);

            return result.FirstOrDefault();
        }

        public async Task<Diet> Update(Diet diet)
        {
            await _diets.ReplaceOneAsync(p => p.Id == diet.Id, diet);
            return diet;
        }
    }
}
