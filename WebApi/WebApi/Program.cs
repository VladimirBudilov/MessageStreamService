using Application.Common.Extensions;
using Serilog;
using Serilog.Events;

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
	builder.Services.AddControllers();
	builder.Services.AddOpenApi();
	builder.Services.AddApplicationLayer();	
	var app = builder.Build();

	if (app.Environment.IsDevelopment())
	{
		app.MapOpenApi();
	}

	app.UseAuthorization();

	app.MapControllers();

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