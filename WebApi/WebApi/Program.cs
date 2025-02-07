using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Messages;
using Serilog;
using Serilog.Events;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
	.CreateLogger();

try
{
	Log.Information("Starting web application");

	builder.Host.UseSerilog();
	builder.Services
		.AddWebApiLayer(builder.Configuration)
		.AddApplicationLayer(Log.Logger)
		.AddInfrastructureLayer(builder.Configuration);

	var app = builder.Build();

	app.UseSwagger();
	app.UseSwaggerUI();

	app.UseCors("AllowAngular");
	app.MapControllers();
	app.MapHub<MessageHub>("/messagehub");
	app.UseExceptionHandler();

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}