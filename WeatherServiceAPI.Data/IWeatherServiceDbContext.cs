using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherServiceAPI.Core.Models;

namespace WeatherServiceAPI.Data
{
    public interface IWeatherServiceDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        public DbSet<IpAddressData> Addresses { get; set; }
        public DbSet<GeolocationData> Locations { get; set; }
        public DbSet<WeatherData> Weather { get; set; }

        public int SaveChanges();
    }
}
 