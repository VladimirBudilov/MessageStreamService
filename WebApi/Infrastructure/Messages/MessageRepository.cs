using System.Data;
using Domain.Messages;
using Infrastructure.Common.Extensions;
using Npgsql;

namespace Infrastructure.Messages;

public class MessageRepository(string connectionString) : IMessageRepository
{
	public async Task SaveMessageAsync(Message message)
	{
		const string query =
			"INSERT INTO Messages (Id, Text, Timestamp) VALUES (@Id, @Text, @Timestamp)";

		await using var connection = new NpgsqlConnection(connectionString);
		await connection.OpenAsync();
		await using var command = new NpgsqlCommand(query, connection)
		{
			CommandType = CommandType.Text
		};

		var messageEntity = message.ToMessageEntity();
		command.Parameters.AddWithValue("Id", messageEntity.Id);
		command.Parameters.AddWithValue("Text", messageEntity.Text);
		command.Parameters.AddWithValue("Timestamp", messageEntity.Timestamp);

		await command.ExecuteNonQueryAsync();
	}

	public async Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(DateTime from, DateTime to)
	{
		const string query =
			"SELECT Id, Text, Timestamp FROM Messages WHERE Timestamp BETWEEN @From AND @To";

		await using var connection = new NpgsqlConnection(connectionString);
		await connection.OpenAsync();

		await using var command = new NpgsqlCommand(query, connection)
		{
			CommandType = CommandType.Text
		};

		command.Parameters.AddWithValue("From", from);
		command.Parameters.AddWithValue("To", to);

		var dataTable = new DataTable();
		using var adapter = new NpgsqlDataAdapter(command);
		adapter.Fill(dataTable);

		var entities = dataTable.AsEnumerable().Select(row => new MessageEntity(
			row.Field<int>("Id"),
			row.Field<string>("Text"),
			row.Field<DateTime>("Timestamp")
		));

		return entities.Select(entity => entity.ToMessage());
	}
}