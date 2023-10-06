using AutoMapper;
using Business.Abstract;
using Business.BusinessRules;
using Business.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public static class BusinessServiceRegistration
	{
		public static IServiceCollection AddBusinessServices(this IServiceCollection services) {

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddScoped<IOperationClaimService, OperationClaimService>();
			services.AddScoped<OperationClaimBusinessRules>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<AuthBusinessRules>();
			services.AddScoped<IUserOperationClaimService, UserOperationClaimService>();





			return services;
		}
	}
}
