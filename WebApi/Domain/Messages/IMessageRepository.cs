namespace Domain.Messages;

public interface IMessageRepository
{
	Task SaveMessageAsync(Message message);
	Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(DateTime from, DateTime to); 
}