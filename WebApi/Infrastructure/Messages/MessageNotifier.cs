using Domain.Messages;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Messages;

public class MessageNotifier(IHubContext<MessageHub, IMessageHubClient> hubContext) : IMessageNotifier
{
	public async Task NotifyMessageAsync(Message message)
	{
		await hubContext.Clients.All.ReceiveMessage(message.ToString());
	}
}