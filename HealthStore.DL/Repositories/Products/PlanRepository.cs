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
        public async Task Delete(int planId)
        {
            await _plans.DeleteOneAsync(p => p.Id == planId);
        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
            var result = await _plans.FindAsync(p => true);
            return result.ToEnumerable();
        }

        public async Task<Plan> GetById(int planId)
        {
            var result = await _plans.FindAsync(p => p.Id == planId);
            return result.FirstOrDefault();
        }

        public async Task<Plan> GetByName(string name)
        {
            var result = await _plans.FindAsync(p => p.Name == name);

            return result.FirstOrDefault();
        }

        public async Task<Plan> Update(Plan plan)
        {
            await _plans.ReplaceOneAsync(p => p.Id == plan.Id, plan);
            return plan;
        }
    }
}
