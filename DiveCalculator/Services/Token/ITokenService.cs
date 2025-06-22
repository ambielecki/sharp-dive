using DiveCalculator.Entities;

namespace DiveCalculator.Services.Token;

public interface ITokenService
{
    string GenerateToken(User user);
}