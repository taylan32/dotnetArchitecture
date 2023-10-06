using Business;
using Core;
using Autofac;
using Core.Exceptions;
using Core.Security.Encryption;
using Core.Security.JWT;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolver.Autofac;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddHttpContextAccessor();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
	containerBuilder.RegisterModule(new AutofacBusinessModule());
});

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidIssuer = tokenOptions.Issuer,
		ValidAudience = tokenOptions.Audience,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
	};

});


//builder.Services.AddAuthorization(options =>
//{
//	options.AddPolicy("AdminOnly", policy => policy.RequireClaim("ADMIN"));
//});

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddBusinessServices();
builder.Services.AddCoreServices();
builder.Services.AddDataAccessServices(builder.Configuration);


var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
