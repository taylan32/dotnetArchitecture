using Core.Persistence.Repositories;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete.Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimRepository
	{
        public UserOperationClaimRepository(BaseDbContext context) : base(context)
        {
            
        }
    }
}
