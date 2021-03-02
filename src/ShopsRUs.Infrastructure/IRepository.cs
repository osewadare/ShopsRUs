using System;
using System.Collections.Generic;
using System.Linq;
using ShopsRus.Core.Entities;

namespace ShopsRUs.Infrastructure
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        bool Insert(T entity);
        bool Update(T entity);
        bool SaveChanges();
        IQueryable<T> SelectQuery(string query);
    }
}
