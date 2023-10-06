using Business.Abstract;
using Business.Dtos.OperationClaimDtos;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OperationClaimController : ControllerBase
	{
		private readonly IOperationClaimService _operationClaimService;

		public OperationClaimController(IOperationClaimService operationClaimService)
		{
			_operationClaimService = operationClaimService;
		}

		[HttpGet("GetList/ByDynamic")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
		{
			var result = await _operationClaimService.GetAll(pageRequest, dynamic);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute] int id)
		{
			var result = await _operationClaimService.Get(id);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateOperationClaimRequestDto request)
		{
			var result = await _operationClaimService.Add(request);
			return Created("/operation-clam/add", result);
		}

		[HttpGet("Test")]
		[Authorize(Policy = "AdminOnly")]
		public IActionResult Test()
		{
			return Ok("WORK");
		}


	}
}
