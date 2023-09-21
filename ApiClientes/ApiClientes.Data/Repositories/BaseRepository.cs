using ApiClientes.Data.Contexts;
using ApiClientes.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        public virtual void Add(TEntity entity)
        {
            using (var context = new DataContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var context = new DataContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var context = new DataContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual List<TEntity> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public virtual TEntity GetById(Guid id)
        {
            using (var context = new DataContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }
    }
}
