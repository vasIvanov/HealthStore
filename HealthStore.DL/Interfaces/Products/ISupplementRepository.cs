using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Interfaces.Products
{
    public interface ISupplementRepository
    {
        Task<Supplement> Create(Supplement supplement);
        Task Delete(int supplementId);
        Task<Supplement> Update(Supplement supplement);
        Task<IEnumerable<Supplement>> GetAll();
        Task<Supplement> GetById(int supplementId);
        Task<Supplement> GetByName(string name);
    }
}
