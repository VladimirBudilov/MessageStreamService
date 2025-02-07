using Domain.Messages;
using Infrastructure.Mapping;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Messages;

public class MessageNotifier(IHubContext<MessageHub, IMessageHubClient> hubContext, MessageMapper mapper) : IMessageNotifier
{
	public async Task NotifyMessageAsync(Message message)
	{
		await hubContext.Clients.All.ReceiveMessage(mapper.ToMessageResponse(message));
	}
}