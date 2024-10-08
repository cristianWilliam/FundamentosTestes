using FudamentosTestes.Db;
using FudamentosTestes.Services;
using Microsoft.EntityFrameworkCore;

namespace FudamentosTestes.Extensions;

internal static class ApiExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        // DI
        services.AddTransient<ICarChassiValidatorService, CarChassiValidatorService>();
        
        // Add DB
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite("DataSource=Fundamentos.db");
        });

        // Add Mediatr
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(ApiExtensions).Assembly));
    }

    public static void InitializeDb(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();
    }
}