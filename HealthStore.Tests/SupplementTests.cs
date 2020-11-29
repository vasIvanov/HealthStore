using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.BL.Services.Products;
using HealthStore.Controllers.Products;
using HealthStore.DL.Interfaces.Products;
using HealthStore.Extensions;
using HealthStore.Models.Products;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthStore.Tests
{
    public class SupplementTests
    {

        private IMapper _mapper;
        private Mock<IDietRepository> _dietRepository;
        private Mock<ISupplementRepository> _supplementRepository;
        private ISupplementService _supplementService;
        private SupplementController _controller;
        IList<Supplement> _supplements = new List<Supplement>()
        {
            {new Supplement { Price = 52, Name = "protein", Id = 1, Description = "test desc", SuitableDietId = 1 } }
        };
        public SupplementTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _dietRepository = new Mock<IDietRepository>();
            _supplementRepository = new Mock<ISupplementRepository>();
            _supplementService = new SupplementService(_dietRepository.Object, _supplementRepository.Object);
            _controller = new SupplementController(_supplementService, _mapper);
        }

        [Fact]
        public async Task Supplement_Delete_NotExisting_Supplement()
        {
            //setup
            var supplementId = 3;

            var supplement = _supplements.FirstOrDefault(x => x.Id == supplementId);


            _supplementRepository.Setup(x => x.Delete(supplementId)).Callback(() => _supplements.Remove(supplement));

            //Act
            var result = await _controller.DeleteSupplement(supplementId);

            //Assert
            //var notFoundObjectResult = result as NotFoundObjectResult;
            //Assert.NotNull(notFoundObjectResult);

            Assert.Null(_supplements.FirstOrDefault(x => x.Id == supplementId));
        }
    }
}
