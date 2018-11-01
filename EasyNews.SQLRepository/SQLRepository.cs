using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.Contracts;
using EasyNews.Core.Models;
using System.Data.Entity;

namespace EasyNews.SQLRepository
{
    public class SQLRepository<T> : IRepository<T> where T : BaseModel
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }


        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            try {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }
            
        }

        public void Update(T ItemToUpdate) {
            dbSet.Attach(ItemToUpdate);
            context.Entry(ItemToUpdate).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            var item = Find(id);
            if (context.Entry(item).State == EntityState.Detached) // Detached = ready to operate on?
                dbSet.Attach(item); //IT's state detached

            dbSet.Remove(item);
        }

        public T Find(string id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T item)
        {
            try { dbSet.Add(item); }
            catch (Exception ex) { }
            
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
