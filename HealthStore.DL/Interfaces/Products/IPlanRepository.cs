using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Interfaces.Products
{
    public interface IPlanRepository
    {
        Task<Plan> Create(Plan plan);
        Task Delete(int planId);
        Task<Plan> Update(Plan plan);
        Task<IEnumerable<Plan>> GetAll();
        Task<Plan> GetById(int planId);
        Task<Plan> GetByName(string name);
    }
}
