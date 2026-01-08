using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.IReposetories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetTableAsNoTracking();
        IQueryable<T> GetTableAsTracking();
    }
}
