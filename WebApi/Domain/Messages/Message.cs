﻿namespace Domain.Messages;

public class Message
{
	private int Id { get; set; }
	private string Text { get; set; }
	private DateTime Timestamp { get; set; }

	public Message(int id, string text)
	{
		if (id <= 0)
			throw new ArgumentException("Id must be greater than zero.", nameof(id));

		if (string.IsNullOrWhiteSpace(text))
			throw new ArgumentException("Message text cannot be empty.", nameof(text));

		if (text.Length > 128)
			throw new ArgumentException("Message text cannot exceed 128 characters.", nameof(text));

		Id = id;
		Text = text;
		Timestamp = DateTime.UtcNow;
	}

	public void UpdateText(string newText)
	{
		if (string.IsNullOrWhiteSpace(newText))
			throw new ArgumentException("Message text cannot be empty.", nameof(newText));

		if (newText.Length > 128)
			throw new ArgumentException("Message text cannot exceed 128 characters.", nameof(newText));

		Text = newText;
	}
	
	public void UpdateTimestamp(DateTime newTimestamp)
	{
		if (newTimestamp < Timestamp)
			throw new ArgumentException("New timestamp cannot be earlier than the current timestamp.", nameof(newTimestamp));
		
		Timestamp = newTimestamp;
	}
	
	public override string ToString()
	{
		return $"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Id}]: {Text}";
	}
	
	public void Deconstruct(out int id, out string text, out DateTime timestamp)
	{
		id = Id;
		text = Text;
		timestamp = Timestamp;
	}
}
