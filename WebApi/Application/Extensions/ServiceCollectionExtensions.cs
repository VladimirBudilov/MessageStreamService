using Application.Messages;
using Domain.Messages;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services, ILogger logger)
	{
		services.AddScoped<IMessageService, MessageService>();
		services.AddScoped<IMessageService>(provider =>
		{
			var messageService = new MessageService(
				provider.GetRequiredService<IMessageRepository>(),
				provider.GetRequiredService<IMessageNotifier>()
			);
			return new LoggingMessageServiceDecorator(messageService, logger);
		});
		return services;
	}
}