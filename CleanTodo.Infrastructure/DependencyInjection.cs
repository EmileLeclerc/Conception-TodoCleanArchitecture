using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTodo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        ));
        services.AddHttpClient("Broker", client =>
        {
            client.BaseAddress = new Uri(
              configuration["Broker:BaseUrl"]
                ?? throw new InvalidOperationException("Broker URL is missing"));
        });
        // Register Repositories
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IShipRepository, ShipRepository>();
        return services;
    }
}