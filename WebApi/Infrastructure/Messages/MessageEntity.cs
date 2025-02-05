namespace Infrastructure.Messages;

public record MessageEntity
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime Timestamp { get; set; }
}