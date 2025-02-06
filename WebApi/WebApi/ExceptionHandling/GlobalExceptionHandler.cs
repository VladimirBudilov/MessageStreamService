using Domain.Messages;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.ExceptionHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		var problemDetails = exception switch
		{
			InvalidMessageException => new ProblemDetails
			{
				Status = StatusCodes.Status400BadRequest,
				Title = "Validation error",
				Detail = exception.Message
			},
			_ => new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Server error",
				Detail = "An unexpected error occurred"
			}
		};
		
		logger.LogError(exception, "An error occurred: {Title} - {Detail}", problemDetails.Title, problemDetails.Detail);
		httpContext.Response.StatusCode = problemDetails.Status!.Value;
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
		return true;
	}
}