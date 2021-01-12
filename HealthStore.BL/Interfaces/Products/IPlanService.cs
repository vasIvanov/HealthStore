using HealthStore.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthStore.BL.Interfaces.Products
{
    public interface IPlanService
    {
        Task<Plan> GetPlanById(int id);
        Task<Plan> GetPlanByName(string name);
        Task<Plan> Create(Plan plan);
        Task<IEnumerable<Plan>> GetAll();
        Task Delete(int id);
        Task<Plan> Update(Plan plan);
        Task<Plan> UpdatePlan(int price, int planId);
    }
}
