using HrELP.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
    }
}
