namespace Domain.Messages;

public interface IMessageService
{
	Task ProcessMessageAsync(Message message);
	Task<IEnumerable<Message>> GetMessagesAsync(DateTime from, DateTime to);
}