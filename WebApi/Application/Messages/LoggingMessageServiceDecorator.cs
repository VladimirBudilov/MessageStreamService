using Domain.Messages;
using Serilog;

namespace Application.Messages;

public class LoggingMessageServiceDecorator(IMessageService inner, ILogger logger) : IMessageService
{
    public async Task ProcessAsync(Message message)
    {
        logger.Information("Starting ProcessMessageAsync for message: {MessageId}", message.Id);
        await inner.ProcessAsync(message);
        logger.Information("Finished ProcessMessageAsync for message: {MessageId}", message.Id);
    }

    public async Task<IEnumerable<Message>> GetAsync(DateTime from, DateTime to)
    {
        logger.Information("Starting GetMessagesAsync from: {From} to: {To}", from, to);
        var result = await inner.GetAsync(from, to);
        logger.Information("Finished GetMessagesAsync from: {From} to: {To}", from, to);
        return result;
    }
}