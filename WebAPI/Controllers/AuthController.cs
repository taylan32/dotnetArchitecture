using Business.Abstract;
using Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
		{
			var result = await _authService.Register(userForRegisterDto);
			return Created("/auth/register", result);
		}


		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
		{
			var result = await _authService.Login(userForLoginDto);
			return Ok(result);
		}


	}
}
