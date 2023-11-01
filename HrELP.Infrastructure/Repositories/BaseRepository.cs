using HrELP.Domain.Entities.Abstract;
using HrELP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly HrElpContext _context;
        protected DbSet<T> _table;

        public BaseRepository(HrElpContext context)
        {
            _context = context;
            _table = context.Set<T>();

        }
        public async Task<int> AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            entity.UpdateDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }
        public async Task DeactivateAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Entry<T>(entity).State = EntityState.Modified;
                entity.UpdateDate = DateTime.Now;
                entity.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ActivateAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Entry<T>(entity).State = EntityState.Modified;
                entity.UpdateDate = DateTime.Now;
                entity.IsActive = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _table.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _table.AsQueryable();
            // Include işlemleri
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            var result = await query.Where(predicate).ToListAsync();
            return result;
        }
    }
}
