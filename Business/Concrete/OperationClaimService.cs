using AutoMapper;
using Business.Abstract;
using Business.BusinessRules;
using Business.Dtos.OperationClaimDtos;
using Business.Models;
using Business.ValidationRules.OperationClaimValidationRules;
using Core.Exceptions;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
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
	public class OperationClaimService : IOperationClaimService
	{

		private readonly IOperationClaimRepository _operationClaimRepository;
		private readonly IMapper _mapper;
		private readonly OperationClaimBusinessRules _operationClaimBusinessRules;


		public OperationClaimService(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
		{
			_operationClaimRepository = operationClaimRepository;
			_mapper = mapper;
			_operationClaimBusinessRules = operationClaimBusinessRules;
		}

		[ValidationAspect(typeof(CreateOperationClaimRequestDtoValidator))]
		public async Task<OperationClaimDto> Add(CreateOperationClaimRequestDto request)
		{
			await _operationClaimBusinessRules.OperationClaimNameCannotBeDuplicated(request.Name);

			OperationClaim addedClaim = await _operationClaimRepository.AddAsync(new()
			{
				Name = request.Name
			}); 
			return _mapper.Map<OperationClaimDto>(addedClaim);
		}

		public async Task<OperationClaimDto> Get(int id)
		{
			OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(item => item.Id == id);
			if(operationClaim == null)
			{
				throw new BusinessException("Requested Operation claim not found", 404);
			}
			return _mapper.Map<OperationClaimDto>(operationClaim);
		}
	

		public async Task<OperationClaimListModel> GetAll(PageRequest pageRequest, Dynamic? dynamic)
		{
			IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListByDynamicAsync(dynamic,
																									index: pageRequest.Page - 1,
																									size: pageRequest.PageSize);	

			return _mapper.Map<OperationClaimListModel>(operationClaims);
		}
	}
}
