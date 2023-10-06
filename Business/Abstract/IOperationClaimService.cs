using Business.Dtos.OperationClaimDtos;
using Business.Models;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IOperationClaimService
	{
		Task<OperationClaimDto> Get(int id);
		Task<OperationClaimListModel> GetAll(PageRequest pageRequest, Dynamic? dynamic);
		Task<OperationClaimDto> Add(CreateOperationClaimRequestDto request);

	}
}
