using Business.Dtos.OperationClaimDtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
	public class OperationClaimListModel : BasePageableModel
	{
		public IList<OperationClaimDto>? Items { get; set; }
	}
}
