using HealthStore.BL.Interfaces.Products;
using HealthStore.DL.Interfaces.Products;
using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Services.Products
{
    public class SupplementService : ISupplementService
    {
        private IDietRepository _dietRepository;
        private ISupplementRepository _supplementRepository;
        public SupplementService(IDietRepository dietRepository, ISupplementRepository supplementRepository)
        {
            _dietRepository = dietRepository;
            _supplementRepository = supplementRepository;
        }
        public async Task<Supplement> GetSupplementById(int id)
        {
            return await _supplementRepository.GetById(id);
        }
        public async Task Delete(int id)
        {
            await _supplementRepository.Delete(id);
        }
        public async Task<IEnumerable<Supplement>> GetAll()
        {
            return await _supplementRepository.GetAll();
        }
        public async Task<Supplement> GetSupplementByName(string name)
        {
            return await _supplementRepository.GetByName(name);
        }
        public async Task<Supplement> Update(Supplement supplement)
        {
            List<Task> tasks = new List<Task>();

            var dietIdExists = _dietRepository.GetById(supplement.SuitableDietId);
            tasks.Add(dietIdExists);
            var idExists = _supplementRepository.GetById(supplement.Id);
            tasks.Add(idExists);
            var uniqueName = _supplementRepository.GetByName(supplement.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);

            if (dietIdExists.Result != null && idExists.Result != null)
            {
                if ((uniqueName.Result != null && uniqueName.Result.Id == supplement.Id) || uniqueName.Result == null)
                {
                    return await _supplementRepository.Update(supplement);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
        public async Task<Supplement> Create(Supplement supplement)
        {
            List<Task> tasks = new List<Task>();

            var dietIdExists = _dietRepository.GetById(supplement.SuitableDietId);
            tasks.Add(dietIdExists);
            var uniqueId = _supplementRepository.GetById(supplement.Id);
            tasks.Add(uniqueId);
            var uniqueName = _supplementRepository.GetByName(supplement.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);

            if (dietIdExists.Result != null && uniqueId.Result == null && uniqueName.Result == null)
            {
                return await _supplementRepository.Create(supplement);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
