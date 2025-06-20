using DiveCalculator.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiveCalculator.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}