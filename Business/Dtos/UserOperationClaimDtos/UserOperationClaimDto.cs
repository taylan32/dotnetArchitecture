using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.UserOperationClaimDtos
{
	public class UserOperationClaimDto
	{
        //public UserDtoForUserOperationClaim User { get; set; }
        //public OperationClaimDtoForUserOperationClaim OperationClaim { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
