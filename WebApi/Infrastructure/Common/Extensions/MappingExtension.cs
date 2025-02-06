using Domain.Messages;
using Infrastructure.Messages;

namespace Infrastructure.Common.Extensions;

public static class MappingExtension
{
	public static MessageEntity ToMessageEntity(this Message message)
	{
		var entity = new MessageEntity();
		(entity.Id, entity.Text, entity.Timestamp) = (message);
		return entity;
	}
	
	public static Message ToMessage(this MessageEntity entity)
	{
		return new Message(entity.Id, entity.Text).WithTimestamp(entity.Timestamp);
	}
	
	private static Message WithTimestamp(this Message message, DateTime timestamp)
	{
		message.UpdateTimestamp(timestamp);
		return message;
	}
	
	public static MessageResponse ToMessageResponse(this Message message)
	{
		var response =  new MessageResponse();
		(response.Id, response.Text, response.Timestamp) = (message);
		return response;
	}
}