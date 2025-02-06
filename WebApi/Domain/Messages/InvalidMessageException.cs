namespace Domain.Messages;

public class InvalidMessageException(string message) : Exception(message);