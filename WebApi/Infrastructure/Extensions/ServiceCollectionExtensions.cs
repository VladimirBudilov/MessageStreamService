using Domain.Messages;
using Infrastructure.Mapping;
using Infrastructure.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddSignalR();
		services.AddScoped<IMessageNotifier, MessageNotifier>();
		services.AddScoped<IMessageRepository, MessageRepository>();
		services.AddSingleton<MessageMapper>();	
		return services;
	}
}