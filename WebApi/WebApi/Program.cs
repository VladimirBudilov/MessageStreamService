using Application.Common.Extensions;
using Infrastructure.Common.Extensions;
using Infrastructure.Messages;
using Serilog;
using Serilog.Events;
using WebApi.Common.Extensions;

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
		.AddWebApiLayer()
		.AddApplicationLayer()
		.AddInfrastructureLayer(builder.Configuration);

	var app = builder.Build();
	
	app.UseCors("AllowAll");
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapControllers();
	app.MapHub<MessageHub>("/messagehub");
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