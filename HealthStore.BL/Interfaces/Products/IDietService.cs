using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Interfaces.Products
{
    public interface IDietService
    {
        Task<Diet> GetDietById(int id);
        Task<Diet> GetDietByName(string name);
        Task<Diet> Create(Diet diet);
        Task<IEnumerable<Diet>> GetAll();
        Task Delete(int id);
        Task<Diet> Update(Diet diet);
        Task<Diet> UpdateDiet(string description, int suitablePlanId, int dietId);
    }
}
