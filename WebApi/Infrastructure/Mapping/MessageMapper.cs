using Domain.Messages;
using Infrastructure.Messages;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Mapping;

[Mapper]
public partial class MessageMapper
{
	public partial MessageEntity ToMessageEntity(Message message);
	public partial Message ToMessage(MessageEntity entity);
	public partial MessageResponse ToMessageResponse(Message message);
	public partial IEnumerable<Message> ToMessage(IEnumerable<MessageEntity> entities);
}