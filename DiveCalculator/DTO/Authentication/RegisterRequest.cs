using System.ComponentModel.DataAnnotations;

namespace DiveCalculator.DTO.Authentication;

public class RegisterRequest
{
    [Required]
    public required string Username { get; set; }
    
    [Required]
    public required string Password { get; set; }
}