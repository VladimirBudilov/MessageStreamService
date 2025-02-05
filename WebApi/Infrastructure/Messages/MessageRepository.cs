using System.Data;
using Domain.Messages;
using Infrastructure.Common.Extensions;
using Npgsql;

namespace Infrastructure.Messages
{
	public class MessageRepository(string connectionString) : IMessageRepository
	{
		public async Task SaveMessageAsync(Message message)
		{
			const string query =
				$"INSERT INTO Messages (Id, Text, Timestamp) VALUES (@{nameof(MessageEntity.Id)}, @{nameof(MessageEntity.Text)}, @{nameof(MessageEntity.Timestamp)}";

			await using var connection = new NpgsqlConnection(connectionString);
			await connection.OpenAsync();
			await using var command = new NpgsqlCommand(query, connection)
			{
				CommandType = CommandType.Text
			};

			var messageEntity = message.ToMessageEntity();
			command.Parameters.AddWithValue(nameof(MessageEntity.Id), messageEntity.Id);
			command.Parameters.AddWithValue(nameof(MessageEntity.Text), messageEntity.Text);
			command.Parameters.AddWithValue(nameof(MessageEntity.Timestamp), messageEntity.Timestamp);

			await command.ExecuteNonQueryAsync();
		}

		public async Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(DateTime from, DateTime to)
		{
			const string query =
				$"SELECT Id, Text, Timestamp FROM Messages WHERE Timestamp BETWEEN @{nameof(from)} AND @{nameof(to)}";

			await using var connection = new NpgsqlConnection(connectionString);
			await connection.OpenAsync();

			await using var command = new NpgsqlCommand(query, connection)
			{
				CommandType = CommandType.Text
			};

			command.Parameters.AddWithValue(nameof(from), from);
			command.Parameters.AddWithValue(nameof(to), to);

			var dataTable = new DataTable();
			using var adapter = new NpgsqlDataAdapter(command);
			adapter.Fill(dataTable);

			var entities = dataTable.AsEnumerable().Select(row => new MessageEntity(
				row.Field<int>(nameof(MessageEntity.Id)),
				row.Field<string>(nameof(MessageEntity.Text)),
				row.Field<DateTime>(nameof(MessageEntity.Timestamp))
			));

			return entities.Select(entity => entity.ToMessage());
		}
	}
}