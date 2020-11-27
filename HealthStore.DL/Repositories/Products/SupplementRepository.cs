using HealthStore.DL.Interfaces.Products;
using HealthStore.Models.Common;
using HealthStore.Models.Products;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthStore.DL.Repositories.Products
{
    public class SupplementRepository : ISupplementRepository
    {
        private readonly IMongoCollection<Supplement> _supplements;
        public SupplementRepository(IOptions<MongoDbConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _supplements = database.GetCollection<Supplement>("Supplement");
        }
        public async Task<Supplement> Create(Supplement supplement)
        {
            await _supplements.InsertOneAsync(supplement);
            return supplement;
        }

        public async Task Delete(int supplementId)
        {
            await _supplements.DeleteOneAsync(p => p.Id == supplementId);
        }

        public async Task<IEnumerable<Supplement>> GetAll()
        {
            var result = await _supplements.FindAsync(p => true);
            return result.ToEnumerable();
        }

        public async Task<Supplement> GetById(int supplementId)
        {
            var result = await _supplements.FindAsync(p => p.Id == supplementId);
            return result.FirstOrDefault();
        }

        public async Task<Supplement> GetByName(string name)
        {
            var result = await _supplements.FindAsync(p => p.Name == name);

            return result.FirstOrDefault();
        }

        public async Task<Supplement> Update(Supplement supplement)
        {
            await _supplements.ReplaceOneAsync(p => p.Id == supplement.Id, supplement);
            return supplement;
        }
    }
}
