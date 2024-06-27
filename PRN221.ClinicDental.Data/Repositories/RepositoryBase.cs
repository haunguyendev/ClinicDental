using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Common.Interface;

using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ClinicDentalDbContext _context { get; set; }

        public RepositoryBase(ClinicDentalDbContext context)
        {
            this._context = context;
        }

        public async Task<IQueryable<T>> FindAllAsync()
        {
            return await Task.FromResult(this._context.Set<T>().AsNoTracking());
        }

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(this._context.Set<T>().Where(expression).AsNoTracking());
        }

        public async Task CreateAsync(T entity)
        {
            await this._context.Set<T>().AddAsync(entity);
        }

        public async Task CreateRange(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => this._context.Set<T>().Update(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => this._context.Set<T>().Remove(entity));
        }

        public async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
