using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Interfaces.Products
{
    public interface ISupplementService
    {
        Task<Supplement> GetSupplementById(int id);
        Task<Supplement> GetSupplementByName(string name);
        Task<Supplement> Create(Supplement supplement);
        Task<IEnumerable<Supplement>> GetAll();
        Task Delete(int id);
        Task<Supplement> Update(Supplement supplement);
        Task<Supplement> UpdateSupplement(string description, int price, int supplementId, int suitableDietId);
    }
}
