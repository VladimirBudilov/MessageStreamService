namespace WebApi.Common.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddOpenApi();
		return services;
	}
}