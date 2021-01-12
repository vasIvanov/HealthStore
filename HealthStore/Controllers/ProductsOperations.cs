using AutoMapper;
using HealthStore.BL.Interfaces.Products;
using HealthStore.Models.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthStore.Controllers
{
    public class ProductsOperations : ControllerBase
    {
		private readonly IDietService _dietService;
		private readonly IPlanService _planService;
		private readonly ISupplementService _supplementService;
		private readonly IMapper _mapper;

		public ProductsOperations(IDietService dietService, IMapper mapper, IPlanService planService, ISupplementService supplementService)
		{
			_dietService = dietService;
			_planService = planService;
			_supplementService = supplementService;
			_mapper = mapper;
		}

		[HttpPost("UpdateDiet")]
		public async Task<IActionResult> UpdateDiet(string description, int suitablePlanId, int dietId)
		{
			var result = await _dietService.UpdateDiet(description, suitablePlanId, dietId);

			if (result == null) return NotFound();

			var diet = _mapper.Map<DietResponse>(result);
			return Ok(diet);
		}

		[HttpPost("UpdatePlan")]
		public async Task<IActionResult> UpdatePlan(int price, int planId)
		{
			var result = await _planService.UpdatePlan(price, planId);

			if (result == null) return NotFound();

			var plan = _mapper.Map<PlanResponse>(result);
			return Ok(plan);
		}

		[HttpPost("UpdateSupplement")]
		public async Task<IActionResult> UpdateSupplement(string description, int price, int supplementId, int suitableDietId)
		{
			var result = await _supplementService.UpdateSupplement(description, price, supplementId, suitableDietId);

			if (result == null) return NotFound();

			var supplement = _mapper.Map<SupplementsResponse>(result);
			return Ok(supplement);
		}
	}
}
