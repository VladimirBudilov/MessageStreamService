using Dapper;
using Domain.Messages;
using Infrastructure.Common.Extensions;
using Npgsql;

namespace Infrastructure.Messages;

public class MessageRepository(string connectionString) : IMessageRepository
{
	public async Task SaveMessageAsync(Message message)
	{
		const string query = "INSERT INTO Messages (Id, Text, Timestamp) VALUES (@Id, @Text, @Timestamp)";

		await using var connection = new NpgsqlConnection(connectionString);
		var messageEntity = message.ToMessageEntity();
		await connection.ExecuteAsync(query, new
		{
			messageEntity.Id,
			messageEntity.Text,
			messageEntity.Timestamp
		});
	}

	public async Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(DateTime from, DateTime to)
	{
		const string query = "SELECT Id, Text, Timestamp FROM Messages WHERE Timestamp BETWEEN @From AND @To";

		await using var connection = new NpgsqlConnection(connectionString);
		var result = await connection.QueryAsync<MessageEntity>(query, new { From = from, To = to });

		return result.Select(entity => entity.ToMessage());
	}
}
