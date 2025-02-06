namespace WebApi.Common.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddSwaggerGen();
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", builder =>
			{
				builder.WithOrigins("http://localhost:4200")
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials();
			});
		});
		return services;
	}
}