﻿using WebApi.ExceptionHandling;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddWebApiLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers();
		services.AddSwaggerGen();
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAngular", builder =>
			{
				builder.WithOrigins(configuration["AllowedOrigins"]!.Split(","))
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials();
			});
		});
		services.AddProblemDetails();
		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddSingleton<MessageMapper>();	
		return services;
	}
}