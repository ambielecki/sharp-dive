using DiveCalculator.Data;
using DiveCalculator.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiveCalculator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task <ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await context.
            Users
            .Select(user => new {
                user.Guid,
                user.Username,
            })
            .ToListAsync();
        
        return Ok(users);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await context.Users
            .Select(user => new {
                user.Guid,
                user.Username,
            })
            .FirstOrDefaultAsync(user => user.Guid == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}