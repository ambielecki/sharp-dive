namespace DiveCalculator.DTO.Authentication;

public class RegisterSuccessResponse
{
    public required string Username { get; set; }
    public required Guid Id { get; set; }
}