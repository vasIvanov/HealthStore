using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.BL.Services.Products;
using HealthStore.Controllers.Products;
using HealthStore.DL.Interfaces.Products;
using HealthStore.Extensions;
using HealthStore.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthStore.Tests
{
    public class DietTests
    {
        private IMapper _mapper;
        private Mock<IDietRepository> _dietRepository;
        private Mock<IPlanRepository> _planRepository;
        private IDietService _dietService;
        private DietController _controller;
        IList<Diet> _diets = new List<Diet>()
        {
            {new Diet { Days = 10, Description = "test desc", DietGoal = "weight loss", Id = 1, Name = "test diet name", Price = 90, SuitablePlanId = 1 } }
        };
        public DietTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _dietRepository = new Mock<IDietRepository>();
            _planRepository = new Mock<IPlanRepository>();
            _dietService = new DietService(_dietRepository.Object, _planRepository.Object);
            _controller = new DietController(_dietService, _mapper);
        }

         [Fact]
        public async Task Diet_Delete_Existing_Diet()
        {
            //setup
            var dietId = 1;

            var diet = _diets.FirstOrDefault(x => x.Id == dietId);


            _dietRepository.Setup(x => x.GetById(dietId)).ReturnsAsync(_diets.FirstOrDefault(x => x.Id == dietId));
            _dietRepository.Setup(x => x.Delete(dietId)).Callback(() => _diets.Remove(diet));

            //Act
            var result = await _controller.DeleteDiet(dietId);

            //Assert
            //var okObjectResult = result as OkObjectResult;
            //Assert.NotNull(okObjectResult);

            Assert.Null(_diets.FirstOrDefault(x => x.Id == dietId));
        }
    }
}
