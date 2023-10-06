using AutoMapper;
using Business.Abstract;
using Business.Models;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
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
	public class UserOperationClaimService : IUserOperationClaimService
	{
		private readonly IUserOperationClaimRepository _userOperationClaimRepository;
		private readonly IMapper _mapper;

		public UserOperationClaimService(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
		{
			_userOperationClaimRepository = userOperationClaimRepository;
			_mapper = mapper;
		}

		public async Task<UserOperationCliamsListModel> GetAll(PageRequest pageRequest, Dynamic? dynamic)
		{
			IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListByDynamicAsync(
				dynamic,
				include: x => x.Include(data => data.User).Include(data => data.OperationClaim),
				index: pageRequest.Page - 1,
				size: pageRequest.PageSize
				);
			//IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
			//			include: x => x.Include(data => data.User).Include(data => data.OperationClaim));

			return _mapper.Map<UserOperationCliamsListModel>(userOperationClaims);
		}
	}
}
