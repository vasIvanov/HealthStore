using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Contracts.Responses;
using HealthStore.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthStore.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private IPlanService _planService;
        private IMapper _mapper;
        public PlanController(IPlanService planService, IMapper mapper)
        {
            _planService = planService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] PlanRequest request)
        {
            if (request == null) return NotFound();
            var plan = _mapper.Map<Plan>(request);
            var result = await _planService.Create(plan);
            if (result == null) return NotFound();
            return Ok(plan);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _planService.GetAll();
            if (result == null) return NotFound("Collection is empty!");
            var response = _mapper.Map<IEnumerable<PlanResponse>>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = await _planService.GetPlanById(id);
            if (employee == null) return NotFound();
            await _planService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlan([FromBody] Plan request)
        {
            if (request == null) return NotFound();
            var plan = _mapper.Map<Plan>(request);
            var result = await _planService.Update(plan);
            if (result == null) return NotFound();
            return Ok(plan);
        }
    }
}
