using Microsoft.EntityFrameworkCore;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeatherServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("weather-service"), b => b.MigrationsAssembly("WeatherServiceAPI.Data")));
builder.Services.AddTransient<IWeatherServiceDbContext, WeatherServiceDbContext>();

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IEntityService<IpAddressData>, EntityService<IpAddressData>>();
builder.Services.AddScoped<IEntityService<GeolocationData>, EntityService<GeolocationData>>();
builder.Services.AddScoped<IEntityService<WeatherData>, EntityService<WeatherData>>();
builder.Services.AddScoped<IIpAddressService, IpAddressService>();
builder.Services.AddScoped<IGeolocationDataService, GeolocationDataService>();
builder.Services.AddScoped<IWeatherDataService, WeatherDataService>();

var app = builder.Build();

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
