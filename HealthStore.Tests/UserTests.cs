using AutoMapper;
using HealthStore.BL.Interfaces.Users;
using HealthStore.BL.Services.Users;
using HealthStore.Controllers.Users;
using HealthStore.DL.Interfaces.Users;
using HealthStore.Extensions;
using HealthStore.Models.Contracts.Responses;
using HealthStore.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthStore.Tests
{
    public class UserTests
    {
        private IMapper _mapper;
        private Mock<IUserRepository> _userRepository;
        private IUserService _userService;
        private UserController _controller;
        IList<User> _users = new List<User>()
        {
            {new User {Id= 1, CreatedAt = new DateTime(), Name = "test"} }
        };
        public UserTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
            _controller = new UserController(_userService, _mapper);
        }
        [Fact]
        public async Task User_GetAll_Count_Check()
        {
            var expectedCount = 1;

            _userRepository.Setup(x => x.GetAll())
               .ReturnsAsync(_users);
            //inject

            //Act
            var result = await _controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var specialties = okObjectResult.Value as IEnumerable<UserResponse>;
            Assert.NotNull(specialties);
            Assert.Equal(expectedCount, specialties.Count());
        }

        [Fact]
        public async Task User_GetById_Data_Check()
        {
            var expectedName = "test";
            var userId = 1;

            _userRepository.Setup(x => x.GetById(userId))
                .ReturnsAsync(_users.FirstOrDefault(x => x.Id == userId));
            var result = await _controller.GetUserById(userId);
            var okObjectResult = result as OkObjectResult;
            var user = okObjectResult.Value as User;
            Assert.NotNull(user);
            Assert.Equal(expectedName, user.Name);
        }
    }
}
