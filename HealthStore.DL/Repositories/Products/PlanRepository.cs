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
    public class PlanRepository : IPlanRepository
    {
        private readonly IMongoCollection<Plan> _plans;
        public PlanRepository(IOptions<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _plans = database.GetCollection<Plan>("Plan");
        }
        public async Task<Plan> Create(Plan plan)
        {
            await _plans.InsertOneAsync(plan);
            return plan;
        }
        public Task Delete(int planId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Plan>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Plan> GetById(int planId)
        {
            throw new NotImplementedException();
        }

        public Task<Plan> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Plan> Update(Plan plan)
        {
            throw new NotImplementedException();
        }
    }
}
