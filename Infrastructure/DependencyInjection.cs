using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories;
using Application.IRepositories.ChangeLog;
using Application.IRepositories.IO;
using Application.IRepositories.LCD;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.IO;
using Application.IServices.LCD;
using Application.IServices.LED;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ChangeLog;
using Infrastructure.Repositories.IO;
using Infrastructure.Repositories.LCD;
using Infrastructure.Repositories.LED;
using Infrastructure.Services;
using Infrastructure.Services.IO;
using Infrastructure.Services.LCD;
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
        ///<summary>
        ///LED injection
        ///</summary>
        services.AddScoped<ILedRepository, LedRepository>();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<ILedModelRepository, LedModelRepository>();
        services.AddScoped<ILedModelService, LedModelService>();
        services.AddScoped<ILedConfigService, LedConfigService>();
        services.AddScoped<ILedConfigRepository, LedConfigRepository>();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<ILineService,LineService>();
        services.AddScoped<ILedStatusRepository, LedStatusRepository>();
        services.AddScoped<ILedStatusService, LedStatusService>();
        services.AddScoped<ILedResultService, LedResultService>();
        services.AddScoped<ILedResultRepository, LedResultRepository>();
        services.AddScoped<ILedRepository, LedRepository>();
        services.AddScoped<ILedService, LedService>();
        ///<summary>
        ///LCD injection
        ///</summary>
        services.AddScoped<ILCDModelRepository,LCDModelRepository>();
        services.AddScoped<ILCDRepository, LCDRepository>();
        services.AddScoped<ILCDModelService, LCDModelService>();
        services.AddScoped<ILCDConfigService,LCDConfigService>();
        services.AddScoped<ILCDConfigRepository, LCDConfigRepository>();
        services.AddScoped<ILCDResultService, LCDResultService>();
        services.AddScoped<ILCDResultRepository, LCDResultRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        ///<summary>
        ///Changelog
        /// </summary>
        services.AddScoped<ILedLogRepository, LedLogRepository>();
        ///<summary>
        ///IO
        /// </summary>
        services.AddScoped<IIOModelRepository, IOModelRepository>();
        services.AddScoped<IIOModelService, IOModelService>();
        services.AddScoped<IIOConfigManagementRepository, IOConfigManagementRepository>();
        services.AddScoped<IIOConfigManagementService, IOConfigManagementService>();
        services.AddScoped<IIORepository, IORepository>();
        services.AddScoped<IIOMotionPointsManagementRepository, IOMotionPointManagementRepository>();
        services.AddScoped<IIOMotionPointManagementService, IOMotionPointManagementService>();
        services.AddScoped<IIOOffsetManagementService, IOOffsetsManagementService>();
        services.AddScoped<IIOOffsetManagementRepository, IOOffsetManagementRepository>();
        services.AddScoped<IIOPressureManagementRepository, IOPressureRepository>();
        services.AddScoped<IIOPressureService, IOPressureService>();
        return services;
    }
}