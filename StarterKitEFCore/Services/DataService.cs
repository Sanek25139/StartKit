using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarterKitEFCore.Model;
using StarterKitEFCore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StarterKitEFCore.Services
{
    public class DataService<TC>(TC context) : IDataService where TC : DbContext
    {
        private readonly TC _context = context;
        private readonly SemaphoreSlim _dbSemaphore = new(1, 1); // Блокировка для всех операций

        public async Task<T> ExecuteDbOperationAsync<T>(Func<TC, Task<T>> operation)
        {
            await _dbSemaphore.WaitAsync();
            try
            {
                return await operation(_context);
            }
            finally
            {
                _dbSemaphore.Release();
            }
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _context.Set<T>();
        }

        public async Task<ObservableCollection<T>> GetAllAsync<T>() where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
            {
                var data = await ctx.Set<T>().ToListAsync();
                return new ObservableCollection<T>(data);
            });
        }

        public async Task<ObservableCollection<T>> GetAllAsync<T>(
            Func<IQueryable<T>, IQueryable<T>>? queryBuilder = null) where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
            {
                IQueryable<T> query = ctx.Set<T>();

                if (queryBuilder != null)
                    query = queryBuilder(query);

                return new ObservableCollection<T>(await query.ToListAsync());
            });
        }

        public async Task<ObservableCollection<T>> GetAllWithIncludeAsync<T>(
            params Expression<Func<T, object>>[] includes) where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
            {
                IQueryable<T> query = ctx.Set<T>();

                foreach (var include in includes)
                    query = query.Include(include);

                return new ObservableCollection<T>(await query.ToListAsync());
            });
        }

        public async Task<ObservableCollection<T>> GetAllWithThenIncludeAsync<T>(params Func<IQueryable<T>, IQueryable<T>>[] includes) where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
            {
                IQueryable<T> query = ctx.Set<T>();

                foreach (var include in includes)
                {
                    query = include(query);
                }

                return new ObservableCollection<T>(await query.ToListAsync());
            });
        }
        public async Task<T?> GetByIdAsync<T>(ulong id) where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
                await ctx.Set<T>().FindAsync(id));
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
            {
                await ctx.Set<T>().AddAsync(entity);
                await ctx.SaveChangesAsync();
                return entity;
            });
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            return await ExecuteDbOperationAsync(async ctx =>
            {
                ctx.Entry(entity).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                return entity;
            });
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync<T>(IEnumerable<T> entity) where T : BaseEntity
        {
            _context.Set<T>().RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entites) where T : BaseEntity
        {
            await _context.Set<T>().AddRangeAsync(entites);
            await _context.SaveChangesAsync();
        }

        public bool HasNavigationProperty<T>(T entity, string navigationPropertyName) where T : BaseEntity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrWhiteSpace(navigationPropertyName)) throw new ArgumentException("Navigation property name cannot be null or empty.", nameof(navigationPropertyName));

            var entry = _context.Entry(entity);
            var navigation = entry.Navigations.FirstOrDefault(n => n.Metadata.Name == navigationPropertyName);

            return navigation != null && navigation.CurrentValue is IEnumerable<object> collection && collection.Any();
        }

        public bool HasNavigationProperty<T>(T entity, string[] navigationPropertyNames) where T : BaseEntity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (navigationPropertyNames == null || navigationPropertyNames.Length == 0) throw new ArgumentException("Navigation property names cannot be null or empty.", nameof(navigationPropertyNames));

            var entry = _context.Entry(entity);
            return navigationPropertyNames.Any(npn =>
            {
                var navigation = entry.Navigations.FirstOrDefault(n => n.Metadata.Name == npn);
                // Проверяем, установлена ли связь и не пустая ли коллекция
                return navigation != null && navigation.CurrentValue is IEnumerable<object> collection && collection.Any();
            });
        }

        public async Task<T?> Find<T>(T entity) where T : BaseEntity
        {
            ArgumentNullException.ThrowIfNull(entity);

            // Проверяем, отслеживается ли объект в контексте
            var localEntity = _context.Set<T>().Local.FirstOrDefault(e => e == entity);
            if (localEntity != null)
            {
                return localEntity; // Возвращаем отслеживаемый объект
            }

            return await _context.Set<T>().FirstOrDefaultAsync(e => e == entity);
        }

        public EntityEntry<T> GetEntry<T>(T entity) where T : BaseEntity
        {
            ArgumentNullException.ThrowIfNull(entity);
            return _context.Entry(entity);
        }
    }
}