using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Repositories;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Mapping;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost", "http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IBloodPressureRepository, BloodPressureRepository>();
builder.Services.AddScoped<IBloodSugarRepository, BloodSugarRepository>();
builder.Services.AddScoped<IBodyTemperatureRepository, BodyTemperatureRepository>();
builder.Services.AddScoped<ISpO2Repository, SpO2Repository>();
builder.Services.AddScoped<INotificationRepository, NotificaitonRepository>();


builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IBloodPressureService, BloodPressureService>();
builder.Services.AddScoped<IBloodSugarService, BloodSugarService>();
builder.Services.AddScoped<IBodyTemperatureService, BodyTemperatureService>();
builder.Services.AddScoped<ISpO2Service, SpO2Service>();
builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
