using Domain.Messages;
using WebApi.Messages.DTOs;

namespace WebApi.Common.Extensions;

public static class MappingExtension
{
	public static MessageResponse ToMessageResponse(this Message message)
	{
		var response =  new MessageResponse();
		(response.Id, response.Text, response.Timestamp) = (message);
		return response;
	}
}