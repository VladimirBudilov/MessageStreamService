using Domain.Messages;
using Serilog;

namespace Application.Messages;

public class LoggingMessageServiceDecorator(IMessageService inner, ILogger logger) : IMessageService
{
    public async Task ProcessMessageAsync(Message message)
    {
        logger.Information("Starting ProcessMessageAsync for message: {MessageId}", message.Id);
        await inner.ProcessMessageAsync(message);
        logger.Information("Finished ProcessMessageAsync for message: {MessageId}", message.Id);
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(DateTime from, DateTime to)
    {
        logger.Information("Starting GetMessagesAsync from: {From} to: {To}", from, to);
        var result = await inner.GetMessagesAsync(from, to);
        logger.Information("Finished GetMessagesAsync from: {From} to: {To}", from, to);
        return result;
    }
}