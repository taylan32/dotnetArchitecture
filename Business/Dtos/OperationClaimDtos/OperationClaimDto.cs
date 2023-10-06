using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.OperationClaimDtos
{
	public class OperationClaimDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }


		public static OperationClaimDto Convert(OperationClaim from)
		{
			return new()
			{
				Id = from.Id,
				Name = from.Name,
			};
		}


	}
}
