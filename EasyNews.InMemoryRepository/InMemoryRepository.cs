using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Cache;
using System.Runtime.Caching; //Needs to be added to reference for inmemory caching
using EasyNews.Core.Models;
using EasyNews.Core.Contracts;

namespace EasyNews.InMemoryRepository
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseModel
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            this.className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null) {
                items = new List<T>();
            }
        }

        public void Update(T itemToFind) {
            T item = items.Find(p => p.localID == itemToFind.localID);
            if (item != null)
            {
                item = itemToFind;
            }
            else
            {
                throw new Exception("Item Not Found");
            }
        }

        public void Insert(T item)
        {
            items.Add(item);
        }
        public void Delete(string id)
        {
            T item = items.Find(p => p.localID == id);

            if (item != null)
            {
                items.Remove(item);
            }
            else
            {
                throw new Exception(className + "couldn't found at InMemoryRepository Delete");
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }
        public T Find(string id)
        {
            T itemToFind = items.Find(p => p.shortUrl == id);

            if (itemToFind != null)
                return itemToFind;
            else
                throw new Exception(className + "couldn't found at InMemoryRepository Find");
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Clear() {
            if (items != null) {
                items.Clear();
            }
        }
    }
}
