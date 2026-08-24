using OpenTelemetry.Metrics;
using ATD_API.Hubs;
using Infrastructure.ExternalServices.Mapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var myAllowSpecificOrigins = "AllowAllWithCredentials";
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllWithCredentials", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)       // Chấp nhận mọi origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Led Model API V1");
        c.RoutePrefix = "swagger"; 
    });
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Led Model API V1");
    c.RoutePrefix = "swagger"; 
});
app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();
app.MapPrometheusScrapingEndpoint("/metrics");
app.Run();