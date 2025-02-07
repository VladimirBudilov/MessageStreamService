using Domain.Messages;
using Domain.Messages.Exceptions;

namespace Application.Messages;

public class MessageService(IMessageRepository messageRepository, IMessageNotifier messageNotifier) : IMessageService
{
	public async Task ProcessAsync(Message message)
	{
		var existingMessage = await messageRepository.GetByIdAsync(message.Id);
		if (existingMessage != null)
			throw new MessageAlreadyExistsException(message.Id);
		await messageRepository.SaveAsync(message);
		await messageNotifier.NotifyMessageAsync(message);
	}

	public async Task<IEnumerable<Message>> GetAsync(DateTime from, DateTime to)
	{
		if (from > to)
			return [];
		return await messageRepository.GetByDateRangeAsync(from, to);
	}
}