using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.DL.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task Delete(int userId);
        Task<User> Update(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int userId);
        Task<User> GetByName(string name);
    }
}
