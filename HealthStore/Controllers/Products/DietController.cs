using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Contracts.Responses;
using HealthStore.Models.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthStore.Controllers.Products
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private IDietService _dietService;
        private IMapper _mapper;
        public DietController(IDietService dietService, IMapper mapper)
        {
            _dietService = dietService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiet([FromBody] DietRequest request)
        {
            if (request == null) return NotFound();
            var plan = _mapper.Map<Diet>(request);
            var result = await _dietService.Create(plan);
            if (result == null) return NotFound();
            return Ok(plan);
        }

        [Authorize(Policy = "ViewDiets")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dietService.GetAll();
            if (result == null) return NotFound("Collection is empty!");
            var response = _mapper.Map<IEnumerable<DietResponse>>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            if (id <= 0) return BadRequest();

            var diet = await _dietService.GetDietById(id);
            if (diet == null) return NotFound();
            await _dietService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiet([FromBody] DietRequest request)
        {
            if (request == null) return NotFound();
            var diet = _mapper.Map<Diet>(request);
            var result = await _dietService.Update(diet);
            if (result == null) return NotFound();
            return Ok(diet);
        }
    }
}
