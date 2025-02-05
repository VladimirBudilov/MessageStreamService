namespace Infrastructure.Messages;

public record MessageEntity()
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime Timestamp { get; set; }
	
	public MessageEntity(int id, string text, DateTime timestamp) : this()
	{
		Id = id;
		Text = text;
		Timestamp = timestamp;
	}
}