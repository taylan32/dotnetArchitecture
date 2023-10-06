using Core.Exceptions;
using Core.Security.Hashing;
using DataAccess.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
	public class AuthBusinessRules
	{
		private readonly IUserRepository _userRepository;

		public AuthBusinessRules(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}


		public async Task EmailShouldBeUniqueWhenRegister(string email)
		{
			User user = await _userRepository.GetAsync(u => u.Email == email);
			if (user == null)
			{
				return;
			}
			throw new BusinessException("Email already in use", 409);
		}

		public async Task<User> EmailAndPasswordShouldMatchWhenLogin(string email, string password)
		{
			User? user = await _userRepository.GetAsync(u => u.Email == email);
			if(user == null || !HashingHelper.VerifyPassowrdHash(password, user.PasswordHash, user.PasswordSalt))
			{
				throw new AuthenticationException("Email or password is wrong");
;			}

			return user;
			
		}

	}
}
