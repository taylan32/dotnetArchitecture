﻿using Core.Persistence.Repositories;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete.Contexts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
	{
		public UserRepository(BaseDbContext context) : base(context)
		{

		}
	}
}
