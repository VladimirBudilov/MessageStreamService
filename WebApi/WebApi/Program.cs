using Application.Extensions;
using Infrastructure.Common.Extensions;
using Infrastructure.Messages;
using Serilog;
using Serilog.Events;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

try
{
	Log.Information("Starting web application");

	builder.Host.UseSerilog();
	builder.Services
		.AddWebApiLayer(builder.Configuration)
		.AddApplicationLayer()
		.AddInfrastructureLayer(builder.Configuration);

	var app = builder.Build();
	
	app.MapOpenApi();
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