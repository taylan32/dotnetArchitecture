using Core.Persistence.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
	{
	}
}
