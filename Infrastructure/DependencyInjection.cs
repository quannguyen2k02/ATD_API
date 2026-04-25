using Application.IRepositories;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.LED;
using Infrastructure.Services;
using Infrastructure.Services.LED;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        });
        services.AddScoped<ILedRepository, LedRepository>();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<ILedModelRepository, LedModelRepository>();
        services.AddScoped<ILedModelService, LedModelService>();
        services.AddScoped<ILedConfigService, LedConfigService>();
        services.AddScoped<ILedConfigRepository, LedConfigRepository>();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<ILineService,LineService>();

        return services;
    }
}