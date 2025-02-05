using System.ComponentModel.DataAnnotations;

namespace WebApi.Messages.DTOs;

public record MessageResponse()
{
	[Required] public int Id { get; set; }
	[Required] public string Text { get; set; }
	[Required] public DateTime Timestamp { get; set; }

	public MessageResponse(int id, string text, DateTime timestamp) : this()
	{
		Id = id;
		Text = text;
		Timestamp = timestamp;
	}
}