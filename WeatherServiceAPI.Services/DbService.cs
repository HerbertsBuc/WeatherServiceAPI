using Microsoft.EntityFrameworkCore;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;

namespace WeatherServiceAPI.Services
{
    public class DbService : IDbService
    {
        protected readonly IWeatherServiceDbContext _context;

        public DbService(IWeatherServiceDbContext context)
        {
            _context = context;
        }

        public T Create<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(s => s.Id == id);
        }
    }
}
