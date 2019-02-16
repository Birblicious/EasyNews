using System.Linq;
using EasyNews.Core.Models;

namespace EasyNews.Core.Contracts
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T item);
        void Update(T item);
        void Clear();
    }
}