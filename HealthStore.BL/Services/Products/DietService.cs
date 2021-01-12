using HealthStore.BL.Interfaces.Products;
using HealthStore.DL.Interfaces.Products;
using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Services.Products
{
    public class DietService : IDietService
    {
        private IDietRepository _dietRepository;
        private IPlanRepository _planRepository;
        public DietService(IDietRepository dietRepository, IPlanRepository planRepository)
        {
            _dietRepository = dietRepository;
            _planRepository = planRepository;
        }
        public async Task<Diet> GetDietById(int id)
        {
            return await _dietRepository.GetById(id);
        }

        public async Task Delete(int id)
        {
            await _dietRepository.Delete(id);
        }
        public async Task<IEnumerable<Diet>> GetAll()
        {
            return await _dietRepository.GetAll();
        }

        public async Task<Diet> GetDietByName(string name)
        {
            return await _dietRepository.GetByName(name);
        }

        public async Task<Diet> Update(Diet diet)
        {
            List<Task> tasks = new List<Task>();

            var planIdExists = _planRepository.GetById(diet.SuitablePlanId);
            tasks.Add(planIdExists);
            var idExists = _dietRepository.GetById(diet.Id);
            tasks.Add(idExists);
            var uniqueName = _dietRepository.GetByName(diet.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);

            if (planIdExists.Result != null && idExists.Result != null)
            {
                if((uniqueName.Result != null && uniqueName.Result.Id == diet.Id) || uniqueName.Result == null)
                {
                    return await _dietRepository.Update(diet);
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

        public async Task<Diet> Create(Diet diet)
        {
            List<Task> tasks = new List<Task>();

            var planIdExists = _planRepository.GetById(diet.SuitablePlanId);
            tasks.Add(planIdExists);
            var uniqueId = _dietRepository.GetById(diet.Id);
            tasks.Add(uniqueId);
            var uniqueName = _dietRepository.GetByName(diet.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);

            if (planIdExists.Result != null && uniqueId.Result == null && uniqueName.Result == null)
            {
                return await _dietRepository.Create(diet);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<Diet> UpdateDiet(string description, int suitablePlanId, int dietId)
        {
            var validPlanId = await _planRepository.GetById(suitablePlanId) != null;
            var diet = await _dietRepository.GetById(dietId);
            if(diet != null && validPlanId)
            {
                diet.Description = description;
                diet.SuitablePlanId = suitablePlanId;
            }

            var result = await _dietRepository.Update(diet);
            return result;
        }
    }
}
