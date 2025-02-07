using Domain.Messages;
using Domain.Messages.Exceptions;

namespace Application.Messages;

public class MessageService(IMessageRepository messageRepository, IMessageNotifier messageNotifier) : IMessageService
{
	public async Task ProcessMessageAsync(Message message)
	{
		var existingMessage = await messageRepository.GetMessageByIdAsync(message.Id);
		if (existingMessage != null)
			throw new MessageAlreadyExistsException(message.Id);
		await messageRepository.SaveMessageAsync(message);
		await messageNotifier.NotifyMessageAsync(message);
	}

	public async Task<IEnumerable<Message>> GetMessagesAsync(DateTime from, DateTime to)
	{
		return await messageRepository.GetMessagesByDateRangeAsync(from, to);
	}
}