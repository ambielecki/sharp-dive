using DiveCalculator.Data;
using DiveCalculator.Services.DiveCalculator;
using DiveCalculator.Services.Token;
using Microsoft.EntityFrameworkCore;

namespace DiveCalculator.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var serverVersion = new MariaDbServerVersion(new Version("10.6.16"));
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseMySql(connectionString, serverVersion);
        });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        services.AddScoped<IDiveCalculator, ImperialDiveCalculator>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}