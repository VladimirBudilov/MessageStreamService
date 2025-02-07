using Domain.Messages;
using Microsoft.AspNetCore.Mvc;
using WebApi.Messages.DTOs;
using MessageMapper = WebApi.Extensions.MessageMapper;

namespace WebApi.Messages;

[ApiController]
[Route("api/[controller]")]
public class MessagesController(IMessageService messageService, MessageMapper mapper) : ControllerBase
{
	[HttpPost]   
	public async Task<IActionResult> CreateMessageAsync(CreateMessageRequest request)
	{
		var message = new Message(request.Id, request.Text);
		await messageService.ProcessAsync(message);
		return Created();
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessagesAsync([FromQuery]DateTime from, DateTime to)
	{    
		var messages = await messageService.GetAsync(from, to);
		var response = mapper.ToMessageResponse(messages);
		return Ok(response);
	}
}