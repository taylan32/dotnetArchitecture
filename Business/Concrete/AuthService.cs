using AutoMapper;
using Business.Abstract;
using Business.BusinessRules;
using Business.Dtos.AuthDtos;
using Business.ValidationRules.AuthValidationRules;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Core.Security.JWT;
using Core.Validation;
using DataAccess.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly AuthBusinessRules _authBusinessRules;
		private readonly ITokenHelper _tokenHelper;
		private readonly IUserOperationClaimRepository _userOperationClaimRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;


		public AuthService(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_authBusinessRules = authBusinessRules;
			_tokenHelper = tokenHelper;
			_userOperationClaimRepository = userOperationClaimRepository;
			_refreshTokenRepository = refreshTokenRepository;
		}


		[ValidationAspect(typeof(UserForLoginDtoValidator))]
		public async Task<LoggedInUserDto> Login(UserForLoginDto userForLoginDto)
		{
			User user = await _authBusinessRules.EmailAndPasswordShouldMatchWhenLogin(userForLoginDto.Email, userForLoginDto.Password);
			string accessToken = await CreateAccessToken(user);
			RefreshToken refreshToken = await GetRefresToken(user);
			return new()
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken.Token
			};

		}


		[ValidationAspect(typeof(UserForRegisterDtoValidator))]
		public async Task<RegisteredUserDto> Register(UserForRegisterDto userForRegisterDto)
		{
			await _authBusinessRules.EmailShouldBeUniqueWhenRegister(userForRegisterDto.Email);

			byte[] passwordHash, passwordSalt;
			HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
			User newUser = new()
			{
				Email = userForRegisterDto.Email,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				Name = userForRegisterDto.FirstName,
				LastName = userForRegisterDto.LastName,
			};

			User createdUser = await _userRepository.AddAsync(newUser);
			string accessToken = await CreateAccessToken(createdUser);
			RefreshToken refreshToken = await CreateRefreshToken(createdUser);
			await AddRefreshToken(refreshToken);
			return new()
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken.Token
			};
		}

		private async Task<string> CreateAccessToken(User user)
		{
			IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id,
				include: u => u.Include(u => u.OperationClaim));

			IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u => new OperationClaim
			{
				Id = u.Id,
				Name = u.OperationClaim.Name
			}).ToList();

			string token = _tokenHelper.CreateToken(user, operationClaims);
			return token;
		}

		private async Task<RefreshToken> CreateRefreshToken(User user)
		{
			RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user);
			return await Task.FromResult(refreshToken);

		}

		private async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
		{
			RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
			return addedRefreshToken;
		}

		private async Task<RefreshToken> GetRefresToken(User user)
		{
			RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(t => t.UserId == user.Id);
			if (refreshToken == null || refreshToken.Expires <= DateTime.UtcNow)
			{
				if(refreshToken != null)
				{
					RefreshToken deletedToken = await _refreshTokenRepository.DeleteAsync(refreshToken);
				}
				RefreshToken token = await CreateRefreshToken(user);
				await AddRefreshToken(token);
				return token;
			}
			return await Task.FromResult(refreshToken);

		}


	}
}
