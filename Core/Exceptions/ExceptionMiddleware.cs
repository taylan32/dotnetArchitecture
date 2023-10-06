﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace Core.Exceptions
{

	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			if(context.Response.StatusCode == 401)
			{

			}
		
			if (ex.GetType() == typeof(BusinessException))
			{
				return CreateBusinessException(context, ex);
			}
			if (ex.GetType() == typeof(ValidationException))
			{
				return CreateValidationException(context, ex);
			}
			if (ex.GetType() == typeof(AuthenticationException))
			{
				return CreateAuthenticationException(context, ex);
			}
			if (ex.GetType() == typeof(AuthorizationException))
			{
				return CreateAuthorizationException(context, ex);
			}

			return CreateInternalException(context, ex);

		}

		private Task CreateAuthorizationException(HttpContext context, Exception ex)
		{
			context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Forbidden);
			return context.Response.WriteAsync(new AuthorizationProblemDetails
			{
				StatusCode = StatusCodes.Status403Forbidden,
				Message = ex.Message,
				Title = "Authorization failed",
				Timestamp = DateTime.Now,
			}.ToString());
		}

		private Task CreateAuthenticationException(HttpContext context, Exception ex)
		{
			context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
			return context.Response.WriteAsync(new AuthenticationProblemDetails
			{
				StatusCode = StatusCodes.Status401Unauthorized,
				Message = ex.Message,
				Title = "Authentication failed",
				Timestamp = DateTime.Now,
			}.ToString());
		}

		private Task CreateValidationException(HttpContext context, Exception ex)
		{
			context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

			List<string> errorMessages = ex.Message
				.Split('\n')
				.Select(message => message.Trim('\r'))
				.ToList();


			return context.Response.WriteAsync(new ValidationProblemDetails
			{
				StatusCode = StatusCodes.Status400BadRequest,
				Message = "Validation failed",
				Title = "Validation Error(s)",
				Timestamp = DateTime.Now,
				Errors = errorMessages
			}.ToString());

		}

		private Task CreateBusinessException(HttpContext context, Exception ex)
		{
			var statusCode = (ex is BusinessException businessException) ? businessException.StatusCode : 400;
			context.Response.StatusCode = Convert.ToInt32(statusCode);
			return context.Response.WriteAsync(new ExceptionDetails
			{
				Timestamp = DateTime.Now,
				Title = "Business Exception",
				Message = ex.Message,
				StatusCode = Convert.ToInt32(statusCode)

			}.ToString());

		}

		private Task CreateInternalException(HttpContext context, Exception ex)
		{
			context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

			return context.Response.WriteAsync(new ExceptionDetails
			{
				Timestamp = DateTime.Now,
				Title = "Internal Server Error",
				Message = ex.Message,
				StatusCode = 500

			}.ToString());
		}


	}

}
