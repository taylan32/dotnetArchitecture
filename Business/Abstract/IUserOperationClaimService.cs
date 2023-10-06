using Business.Dtos.UserOperationClaimDtos;
using Business.Models;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserOperationClaimService
	{
		Task<UserOperationCliamsListModel> GetAll(PageRequest pageRequest, Dynamic? dynamic);
		Task<UserOperationClaimDto> GetByIdAsync();
	}
}
