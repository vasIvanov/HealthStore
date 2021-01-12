using HealthStore.BL.Interfaces.Products;
using HealthStore.DL.Interfaces.Products;
using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Services.Products
{
    public class PlanService : IPlanService
    {
        private IPlanRepository _planRepository;
        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task<Plan> Create(Plan plan)
        {
            List<Task> tasks = new List<Task>();
            var uniqueId = _planRepository.GetById(plan.Id);
            tasks.Add(uniqueId);
            var uniqueName = _planRepository.GetByName(plan.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);
            if (uniqueId.Result == null && uniqueName.Result == null)
            {
                return await _planRepository.Create(plan);
            }
            else
            {
                throw new Exception();
            }
        }
        public async Task Delete(int id)
        {
            await _planRepository.Delete(id);
        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
            return await _planRepository.GetAll();
        }

        public async Task<Plan> GetPlanById(int id)
        {
            return await _planRepository.GetById(id);
        }

        public async Task<Plan> GetPlanByName(string name)
        {
            return await _planRepository.GetByName(name);
        }

        public async Task<Plan> Update(Plan plan)
        {
            List<Task> tasks = new List<Task>();
            var idExists = _planRepository.GetById(plan.Id);
            tasks.Add(idExists);
            var uniqueName = _planRepository.GetByName(plan.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);
            if (idExists.Result != null)
            {
                if ((uniqueName.Result != null && uniqueName.Result.Id == plan.Id) || uniqueName.Result == null)
                {
                    return await _planRepository.Update(plan);
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

        public async Task<Plan> UpdatePlan(int price, int planId)
        {
            var plan = await _planRepository.GetById(planId);
            if (plan != null)
            {
                plan.Price = price;
            }

            var result = await _planRepository.Update(plan);
            return result;
        }
    }
}
