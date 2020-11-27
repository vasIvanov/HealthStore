using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.BL.Services.Products;
using HealthStore.Controllers.Products;
using HealthStore.DL.Interfaces.Products;
using HealthStore.Extensions;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Contracts.Responses;
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
    public class PlanTests
    {
        private IMapper _mapper;
        private Mock<IPlanRepository> _planRepository;
        private IPlanService _planService;
        private PlanController _controller;
        IList<Plan> _plans = new List<Plan>()
        {
            {new Plan {Name = "test plan name", Id = 1, Days = 11, Exercices = new [] {"test ex1", "test ex2" }, Price = 123} }
        };
        public PlanTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _planRepository = new Mock<IPlanRepository>();
            _planService = new PlanService(_planRepository.Object);
            _controller = new PlanController(_planService, _mapper);
        }

        [Fact]
        public async Task Plan_Update_Name()
        {
            var planId = 1;
            var expectedPlanName = "New Position Name";

            var plan = _plans.FirstOrDefault(x => x.Id == planId);
            plan.Name = expectedPlanName;

            _planRepository.Setup(x => x.Update(plan)).ReturnsAsync(_plans.FirstOrDefault(x => x.Id == planId));

            //Act
            var result = await _controller.UpdatePlan(plan);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var pos = okObjectResult.Value as Plan;
            Assert.NotNull(pos);
            Assert.Equal(expectedPlanName, pos.Name);
        }
    }
}
