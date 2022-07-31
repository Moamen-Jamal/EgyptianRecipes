using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Generic<T> where T : BaseModel
    {
        private DbSet<T> dbSet;
        public EntitiesContext Context { get; set; }
        public Generic(EntitiesContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }

        public T Add(T T)
        {
            return dbSet.Add(T);
        }
        public T Update(T T)
        {
            if (!dbSet.Local.Any(i => i.ID == T.ID))
                dbSet.Attach(T);
            Context.Entry(T).State = EntityState.Modified;
            return T;
        }
        public T Remove(T T)
        {
            T selected = dbSet.Find(T.ID);
            dbSet.Remove(selected);
            return T;
        }
        public T GetByID(int id)
        {
            return dbSet.FirstOrDefault(i => i.ID == id);
        }
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter);
        }
    }
}
