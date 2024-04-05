using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(SocialNetworkDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            return Task.FromResult(true);
        }
        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
