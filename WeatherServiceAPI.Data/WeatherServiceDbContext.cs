using Microsoft.EntityFrameworkCore;
using WeatherServiceAPI.Core.Models;

namespace WeatherServiceAPI.Data
{
    public class WeatherServiceDbContext : DbContext
    {
        public WeatherServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<IPAddressData> Addresses { get; set; }
        public DbSet<GeolocationData> Locations { get; set; }
        public DbSet<WeatherData> Weather { get; set; }
    }
}
