using AutoMapper;
using Business.Dtos.OperationClaimDtos;
using Business.Dtos.UserOperationClaimDtos;
using Business.Models;
using Core.Persistence.Paging;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
	public class MappingProfiles : Profile
	{

		public MappingProfiles()
		{
			CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
			CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();

			CreateMap<UserOperationClaim, UserOperationClaimDto>()
				.ForMember(x => x.UserName, data => data.MapFrom(c => c.User.Name))
				.ForMember(x => x.UserId, data => data.MapFrom(c => c.User.Id))
				.ForMember(x => x.RoleName, data => data.MapFrom(c => c.OperationClaim.Name))
				.ReverseMap();

			CreateMap<IPaginate<UserOperationClaim>, UserOperationCliamsListModel>().ReverseMap();

		}

	}
}
