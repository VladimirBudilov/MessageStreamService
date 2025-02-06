namespace Domain.Messages;

public class Message
{
	public int Id { get; private set; }
	public string Text { get; private set; }
	public DateTime Timestamp { get; private set; }

	public Message(int id, string text)
	{
		if (id <= 0)
			throw new InvalidMessageException("Id must be greater than zero.");

		if (string.IsNullOrWhiteSpace(text))
			throw new InvalidMessageException("Message text cannot be empty.");

		if (text.Length > 128)
			throw new InvalidMessageException("Message text cannot exceed 128 characters.");

		Id = id;
		Text = text;
		Timestamp = DateTime.UtcNow;
	}

	public override string ToString() =>
		$"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Id}]: {Text}";
}
