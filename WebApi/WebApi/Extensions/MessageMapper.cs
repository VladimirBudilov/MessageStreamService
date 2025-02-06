using Domain.Messages;
using Riok.Mapperly.Abstractions;
using WebApi.Messages.DTOs;

namespace WebApi.Extensions;

[Mapper]
public partial class MessageMapper
{
	public partial MessageResponse ToMessageResponse(Message message);
	public partial IEnumerable<MessageResponse> ToMessageResponse(IEnumerable<Message> messages);
}