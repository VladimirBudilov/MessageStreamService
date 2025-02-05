using Domain.Messages;

namespace Infrastructure.Messages;

public class MessageRepository : IMessageRepository
{
	public Task SaveMessageAsync(Message message)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(DateTime from, DateTime to)
	{
		throw new NotImplementedException();
	}
}