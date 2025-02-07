using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Messages;

public record MessageResponse
{
	[Required] public int Id { get; set; }
	[Required] public string Text { get; set; }
	[Required] public DateTime Timestamp { get; set; }
}