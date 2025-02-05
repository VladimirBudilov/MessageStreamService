namespace Domain.Messages
{
	public class Message
	{
		private readonly int _id;
		private string _text;
		private DateTime _timestamp;

		public Message(int id, string text)
		{
			if (id <= 0)
				throw new ArgumentException("Id must be greater than zero.", nameof(id));

			if (string.IsNullOrWhiteSpace(text))
				throw new ArgumentException("Message text cannot be empty.", nameof(text));

			if (text.Length > 128)
				throw new ArgumentException("Message text cannot exceed 128 characters.", nameof(text));

			_id = id;
			_text = text;
			_timestamp = DateTime.UtcNow;
		}

		public void UpdateText(string newText)
		{
			if (string.IsNullOrWhiteSpace(newText))
				throw new ArgumentException("Message text cannot be empty.", nameof(newText));

			if (newText.Length > 128)
				throw new ArgumentException("Message text cannot exceed 128 characters.", nameof(newText));

			_text = newText;
		}

		public void UpdateTimestamp(DateTime newTimestamp)
		{
			if (newTimestamp < _timestamp)
				throw new ArgumentException("New timestamp cannot be earlier than the current timestamp.", nameof(newTimestamp));

			_timestamp = newTimestamp;
		}

		public override string ToString()
		{
			return $"{_timestamp:yyyy-MM-dd HH:mm:ss} [{_id}]: {_text}";
		}

		public void Deconstruct(out int id, out string text, out DateTime timestamp)
		{
			id = _id;
			text = _text;
			timestamp = _timestamp;
		}
	}
}