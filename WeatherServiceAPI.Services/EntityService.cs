using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;

namespace WeatherServiceAPI.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IWeatherServiceDbContext context) : base(context)
        {
        }

        public T Create(T entity)
        {
            return Create<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }

        public List<T> GetAll()
        {
            return GetAll<T>();
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }
        public T GetById(int id)
        {
            return GetById<T>(id);
        }
    }
}
