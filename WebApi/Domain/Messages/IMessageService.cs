namespace Domain.Messages;

public interface IMessageService
{
	Task ProcessAsync(Message message);
	Task<IEnumerable<Message>> GetAsync(DateTime from, DateTime to);
}