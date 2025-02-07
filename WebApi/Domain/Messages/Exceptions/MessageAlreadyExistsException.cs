namespace Domain.Messages.Exceptions;

public class MessageAlreadyExistsException(int id) : Exception($"Message {id} already exists.");