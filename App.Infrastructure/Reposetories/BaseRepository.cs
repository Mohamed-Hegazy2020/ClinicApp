using App.Domain.IReposetories;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Reposetories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
      
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            int rows= await _dbContext.SaveChangesAsync();
            return rows;
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            int rows = await _dbContext.SaveChangesAsync();
            return rows;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            int rows = await _dbContext.SaveChangesAsync();
            return rows;
        }
        public IQueryable<T> GetTableAsNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }
        public IQueryable<T> GetTableAsTracking()
        {
            return _dbContext.Set<T>().AsQueryable();

        }

    }

}
