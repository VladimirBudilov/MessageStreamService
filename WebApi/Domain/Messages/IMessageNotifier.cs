namespace Domain.Messages;

public interface IMessageNotifier
{
	Task NotifyMessageAsync(Message message);
}