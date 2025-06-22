using DiveCalculator.Data;
using DiveCalculator.Services.DiveCalculator;
using DiveCalculator.Services.Token;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MariaDbServerVersion(new Version("10.6.16"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseMySql(connectionString, serverVersion);
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IDiveCalculator, ImperialDiveCalculator>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();