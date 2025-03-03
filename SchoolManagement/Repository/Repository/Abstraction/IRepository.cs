﻿using DomainModels.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstraction
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression, IList<string> includedProps = null);
        IQueryable<T> GetAllAsyncAsNoTracking();
        Task<T> GetAsync(int id);
        Task<bool> AddAsync(T item);
        Task<bool> AddAsync(List<T> items);
        Task<bool> AddAsync(params T[] items);
        bool Update(T item);
        bool Update(List<T> items);
        bool Update(params T[] items);
        Task<bool> DeleteAsync(T item);
        Task<bool> DeleteAsync(List<T> items);
        Task<bool> DeleteAsync(params T[] items);
    }
}
