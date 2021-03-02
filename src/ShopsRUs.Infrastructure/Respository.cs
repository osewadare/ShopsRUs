using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopsRus.Core.Entities;
using ShopsRus.Infrastructure;

namespace ShopsRUs.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ShopContext context;
        private DbSet<T> entities;

        public Repository(ShopContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public bool Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            return context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            return context.SaveChanges() > 0;
        }

        public bool SaveChanges()
        {
           return context.SaveChanges() > 0;
        }

        public IQueryable<T> SelectQuery(string query)
        {
            return entities.FromSql(query);
        }
    }
}  
