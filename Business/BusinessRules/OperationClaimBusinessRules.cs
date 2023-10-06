using Core.Exceptions;
using DataAccess.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
	public class OperationClaimBusinessRules
	{
		private readonly IOperationClaimRepository _operationClaimRepository;

		public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
		{
			_operationClaimRepository = operationClaimRepository;
		}

		public async Task OperationClaimNameCannotBeDuplicated(string name)
		{
			OperationClaim claim = await _operationClaimRepository.GetAsync(c => c.Name == name);
			if (claim != null)
			{
				throw new BusinessException("Operation claim name exists.", 409);
			}

		}


	}
}
