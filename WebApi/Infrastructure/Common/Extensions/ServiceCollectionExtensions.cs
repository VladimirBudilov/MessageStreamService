using Domain.Messages;
using Infrastructure.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Common.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddScoped<IMessageNotifier, MessageNotifier>();
		services.AddScoped<IMessageRepository>(provider =>
			new MessageRepository(configuration.GetConnectionString("DefaultConnection")!) ??
			throw new ArgumentNullException(nameof(MessageRepository)));

		return services;
	}
}