using Application.Messages;
using Domain.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		services.AddScoped<IMessageService, MessageService>();
		return services;
	}
}