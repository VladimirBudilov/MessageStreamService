﻿using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Messages;

public class MessageHub : Hub<IMessageHubClient>
{
	public async Task SendMessageToClients(MessageResponse message)
	{
		await Clients.All.ReceiveMessage(message);
	}
}

public interface IMessageHubClient
{
	Task ReceiveMessage(MessageResponse message);
}