using Business.Abstract;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserOperationClaimController : ControllerBase
	{
		private readonly IUserOperationClaimService _userOperationClaimService;

		public UserOperationClaimController(IUserOperationClaimService userOperationClaimService)
		{
			_userOperationClaimService = userOperationClaimService;
		}

		[HttpGet]
		public async Task<IActionResult> Test([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
		{
			return Ok(await _userOperationClaimService.GetAll(pageRequest, dynamic));
		}
	}
}
