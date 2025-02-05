using Domain.Messages;

namespace Infrastructure.Messages;

public class MessageNotifier : IMessageNotifier
{
	public Task NotifyMessageAsync(Message message)
	{
		throw new NotImplementedException();
	}
}