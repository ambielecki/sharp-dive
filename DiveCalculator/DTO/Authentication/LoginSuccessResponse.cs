namespace DiveCalculator.DTO.Authentication;

public class LoginSuccessResponse
{
    public required string Username { get; set; }
    public required Guid Id { get; set; }
    public required string Token { get; set; }
}