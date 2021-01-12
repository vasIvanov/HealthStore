using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Interfaces.Users
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<User> Create(User user);
        Task<IEnumerable<User>> GetAll();
        Task Delete(int id);
        Task<User> Update(User user);
        Task<User> UpdateUserName(int userId, string name);

    }
}
