using System.ComponentModel.DataAnnotations;

namespace DiveCalculator.DTO.Authentication;

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}