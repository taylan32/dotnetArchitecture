using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public static class CoreServiceRegistration
	{
		public static IServiceCollection AddCoreServices(this IServiceCollection services)
		{
			services.AddScoped<ITokenHelper, JwtHelper>();
			return services;
		}
	}
}
