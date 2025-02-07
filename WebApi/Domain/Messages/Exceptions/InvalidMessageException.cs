namespace Domain.Messages.Exceptions;

public class InvalidMessageException(string message) : Exception(message);