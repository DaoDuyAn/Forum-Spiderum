﻿using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();

    }
}
