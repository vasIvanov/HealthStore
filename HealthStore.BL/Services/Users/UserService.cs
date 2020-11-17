using HealthStore.BL.Interfaces.Users;
using HealthStore.DL.Interfaces.Users;
using HealthStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthStore.BL.Services.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Create(User user)
        {
            List<Task> tasks = new List<Task>();
            var uniqueId = _userRepository.GetById(user.Id);
            tasks.Add(uniqueId);
            var uniqueName = _userRepository.GetByName(user.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);
            if(uniqueId.Result == null && uniqueName.Result == null)
            {
                return await _userRepository.Create(user);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async  Task<User> GetUserByName(string name)
        {
            return await _userRepository.GetByName(name);
        }

        public async Task<User> Update(User user)
        {
            List<Task> tasks = new List<Task>();
            var idExists = _userRepository.GetById(user.Id);
            tasks.Add(idExists);
            var uniqueName = _userRepository.GetByName(user.Name);
            tasks.Add(uniqueName);

            await Task.WhenAll(tasks);
            if(idExists.Result != null && uniqueName.Result == null)
            {
                return await _userRepository.Update(user);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
