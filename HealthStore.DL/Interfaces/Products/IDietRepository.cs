using HealthStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Interfaces.Products
{
    public interface IDietRepository
    {
        Task<Diet> Create(Diet diet);
        Task Delete(int dietId);
        Task<Diet> Update(Diet diet);
        Task<IEnumerable<Diet>> GetAll();
        Task<Diet> GetById(int dietId);
        Task<Diet> GetByName(string name);
    }
}
