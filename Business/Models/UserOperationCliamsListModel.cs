
using Business.Dtos.UserOperationClaimDtos;
using Core.Persistence.Paging;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
	public class UserOperationCliamsListModel: BasePageableModel
	{
		public IList<UserOperationClaimDto>? Items { get; set; }
	}
}
