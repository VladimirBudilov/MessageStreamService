using System.Data;
using Domain.Messages;
using Infrastructure.Mapping;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Messages;

public class MessageRepository(IConfiguration configuration, MessageMapper mapper) : IMessageRepository
{
	private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ??
	                                            throw new ArgumentNullException(nameof(MessageRepository));

	public async Task SaveAsync(Message message)
	{
		const string query =
			"INSERT INTO Messages (Id, Text, Timestamp) VALUES (@Id, @Text, @Timestamp)";

		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync();
		await using var transaction = await connection.BeginTransactionAsync();

		try
		{
			await using var command = new NpgsqlCommand(query, connection, transaction)
			{
				CommandType = CommandType.Text
			};

			var messageEntity = mapper.ToMessageEntity(message);
			command.Parameters.AddWithValue("Id", messageEntity.Id);
			command.Parameters.AddWithValue("Text", messageEntity.Text);
			command.Parameters.AddWithValue("Timestamp", messageEntity.Timestamp);

			await command.ExecuteNonQueryAsync();

			await transaction.CommitAsync();
		}
		catch
		{
			await transaction.RollbackAsync();
			throw;
		}
	}


	public async Task<IEnumerable<Message>> GetByDateRangeAsync(DateTime from, DateTime to)
	{
		const string query =
			"SELECT Id, Text, Timestamp FROM Messages WHERE Timestamp BETWEEN @From AND @To";

		await using var connection = new NpgsqlConnection(_connectionString);
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

		return mapper.ToMessage(entities);
	}

	public async Task<Message?> GetByIdAsync(int messageId)
	{
		const string query = "SELECT Id, Text, Timestamp FROM Messages WHERE Id = @Id";
		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync();

		await using var command = new NpgsqlCommand(query, connection)
		{
			CommandType = CommandType.Text
		};

		command.Parameters.AddWithValue("Id", messageId);
		await using var reader = await command.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
		{
			return null;
		}

		var messageEntity = new MessageEntity(
			reader.GetInt32(0),
			reader.GetString(1),
			reader.GetDateTime(2)
		);

		return mapper.ToMessage(messageEntity);
	}
}