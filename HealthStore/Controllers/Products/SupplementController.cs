using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Contracts.Responses;
using HealthStore.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthStore.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplementController : ControllerBase
    {
        private ISupplementService _supplementService;
        private IMapper _mapper;
        public SupplementController(ISupplementService supplementService, IMapper mapper)
        {
            _supplementService = supplementService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplement([FromBody] SupplementsRequest request)
        {
            if (request == null) return NotFound();
            var supplement = _mapper.Map<Supplement>(request);
            var result = await _supplementService.Create(supplement);
            if (result == null) return NotFound();
            return Ok(supplement);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _supplementService.GetAll();
            if (result == null) return NotFound("Collection is empty!");
            var response = _mapper.Map<IEnumerable<SupplementsResponse>>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSupplement(int id)
        {
            if (id <= 0) return BadRequest();

            var diet = await _supplementService.GetSupplementById(id);
            if (diet == null) return NotFound();
            await _supplementService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplement([FromBody] SupplementsRequest request)
        {
            if (request == null) return NotFound();
            var supplement = _mapper.Map<Supplement>(request);
            var result = await _supplementService.Update(supplement);
            if (result == null) return NotFound();
            return Ok(supplement);
        }
    }
}
