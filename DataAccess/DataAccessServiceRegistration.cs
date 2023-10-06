using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.Repositories.Concrete.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public static class DataAccessServiceRegistration
	{
		public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ProjectConnectionString")));

			services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

			return services;
		}
	}
}
