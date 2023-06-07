using Microsoft.EntityFrameworkCore;
using WeatherServiceAPI.Core.Models;

namespace WeatherServiceAPI.Data
{
    public interface IWeatherServiceDbContext
    {
        public DbSet<IPAddressData> Addresses { get; set; }
        public DbSet<GeolocationData> Locations { get; set; }
        public DbSet<WeatherData> Weather { get; set; }

        public int SaveChanges();
    }
}
 