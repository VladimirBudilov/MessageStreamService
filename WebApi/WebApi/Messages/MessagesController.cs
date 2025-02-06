using Domain.Messages;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Messages.DTOs;

namespace WebApi.Messages;

[ApiController]
[Route("api/[controller]")]
public class MessagesController(IMessageService messageService, IMessageNotifier messageNotifier) : ControllerBase
{
	[HttpPost]   
	public async Task<IActionResult> CreateMessageAsync(CreateMessageRequest request)
	{
		var message = new Message(request.Id, request.Text);
		await messageService.ProcessMessageAsync(message);
		return Created();
	}

	[HttpGet]
	public async Task<ActionResult<MessageResponse>> GetMessagesAsync([FromQuery]DateTime from, DateTime to)
	{
		var messages = await messageService.GetMessagesAsync(from, to);
		var response = messages.Select(m => m.ToMessageResponse());
		return Ok(response);
	}
}