using System.Security.Cryptography;
using System.Text;
using DiveCalculator.Data;
using DiveCalculator.DTO.Authentication;
using DiveCalculator.Entities;
using DiveCalculator.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiveCalculator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(DataContext context, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisterSuccessResponse>> Register(RegisterRequest request) {
        if (await UserExists(request.Username)) {
            return BadRequest("Username already exists");       
        }

        using var hmac = new HMACSHA512();
        var user = new User {
            Username = request.Username.ToLower(),
            Guid = Guid.CreateVersion7(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return Ok(new RegisterSuccessResponse {
            Username = user.Username,
            Id = user.Guid
        });
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginSuccessResponse>> Login(LoginRequest request) {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Username == request.Username.ToLower());
        if (user == null) {
            return Unauthorized("Invalid username or password");
        }
        
        var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        for (var i = 0; i < computedHash.Length; i++) {
            if (computedHash[i] != user.PasswordHash[i]) {
                return Unauthorized("Invalid username or password");
            }       
        }
        
        return Ok(new LoginSuccessResponse {
            Username = user.Username, 
            Id = user.Guid, 
            Token = tokenService.GenerateToken(user)
        });
    }

    private async Task<bool> UserExists(string username) {
        return await context.Users.AnyAsync(user => user.Username.ToLower() == username.ToLower());
    }
}