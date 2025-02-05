using System.ComponentModel.DataAnnotations;

namespace WebApi.Messages.DTOs;

public record CreateMessageRequest([Required] int Id,[Required] string Text);