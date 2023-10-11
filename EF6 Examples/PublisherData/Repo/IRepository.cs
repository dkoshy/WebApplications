using Microsoft.EntityFrameworkCore;

namespace PublisherData.Repo
{
    public interface IRepository<T> where T : class?
    {
        IQueryable<T> GetAll();
        Task<T> FindAsync<Tkey>( Tkey id) where Tkey : struct;
        Task<T> UpdateAsync(T item);
        Task AddAsync(T  item);
        Task RemoveAsync(T item);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PublisherDBContext _dbCtx;
        private readonly DbSet<T> _dbSet;
        public Repository(PublisherDBContext dBContext)
        {
           _dbCtx = dBContext;
           _dbSet = _dbCtx.Set<T>();
        }
        public virtual async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await SaveChangesAsync();

        }

        public virtual async Task<T?> FindAsync<Tkey>(Tkey id) where Tkey : struct
        {
            var result =  await _dbSet.FindAsync(id);
            return result; 
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task RemoveAsync(T item)
        {
           _dbSet.Remove(item);
            await SaveChangesAsync();
        }

        public virtual async  Task<T> UpdateAsync(T item)
        {
            _dbSet.Update(item);
            await SaveChangesAsync();
            return item;
        }

        private  async Task SaveChangesAsync()
        {
            await _dbCtx.SaveChangesAsync();
        }
    }


}
