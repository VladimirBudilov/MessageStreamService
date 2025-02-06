using Domain.Messages;
using Infrastructure.Common.Extensions;
using Infrastructure.Mapping;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Messages;

public partial class MessageNotifier(IHubContext<MessageHub, IMessageHubClient> hubContext, MessageMapper mapper) : IMessageNotifier
{
	public async Task NotifyMessageAsync(Message message)
	{
		await hubContext.Clients.All.ReceiveMessage(mapper.ToMessageResponse(message));
	}
}