namespace Domain.Messages;

public interface IMessageRepository
{
	Task SaveAsync(Message message);
	Task<IEnumerable<Message>> GetByDateRangeAsync(DateTime from, DateTime to);
	Task<Message?> GetByIdAsync(int messageId);
}