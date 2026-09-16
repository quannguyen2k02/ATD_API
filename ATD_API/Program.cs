using ATD_API.Hubs;
using Infrastructure.ExternalServices.Mapper;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. Cấu hình giới hạn upload file lớn (Kestrel)
// ============================================================
builder.WebHost.ConfigureKestrel(options =>
{
    // 500 MB - điều chỉnh theo nhu cầu thực tế
    options.Limits.MaxRequestBodySize = 2000L * 1024 * 1024;
});

// ============================================================
// 2. Cấu hình FormOptions cho multipart/form-data
// ============================================================
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2000L * 1024 * 1024; // 500 MB
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
    options.BufferBodyLengthLimit = 2000L * 1024 * 1024;
});

// ============================================================
// 3. CORS
// ============================================================
var myAllowSpecificOrigins = "AllowAllWithCredentials";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins, policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ============================================================
// 4. Controllers, Swagger, SignalR
// ============================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// ============================================================
// 5. Đăng ký các service của project
// ============================================================
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

var app = builder.Build();

// ============================================================
// 6. Swagger (chỉ cấu hình 1 lần, không lặp lại)
// ============================================================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Led Model API V1");
    c.RoutePrefix = "swagger";
});

// ============================================================
// 7. Middleware pipeline
// ============================================================
app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();

app.Run();