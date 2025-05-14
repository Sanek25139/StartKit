
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarterKit.EF.Model;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace StarterKit.EF.Services.Interface
{
    public interface IDataService
    {
        [Obsolete("Этот метод не защищён семафором и может вызывать race conditions. Используйте GetAllAsync вместо него (и с методом LinqAsync если нужен спецефический запрос).")]
        IQueryable<T> GetAll<T>() where T : class;
        /// <summary>
        /// Возвращает коллекцию сущностей. Для конвертации используйте .ToObservable()/ToList()/ToArray().
        /// Пример: await GetAllAsync<Student>().ToObservable();
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        /// <summary>
        /// Возвращает коллекцию сущностей. Для конвертации используйте .ToObservable()/ToList()/ToArray().
        /// Пример: await GetAllAsync<Student>().ToObservable();
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync<T>(Func<IQueryable<T>, IQueryable<T>>? queryBuilder = null) where T : class;
        /// <summary>
        /// Возвращает коллекцию сущностей. Для конвертации используйте .ToObservable()/ToList()/ToArray().
        /// Пример: await GetAllAsync<Student>().ToObservable();
        /// </summary>
        Task<IEnumerable<T>> GetAllWithIncludeAsync<T>(params Expression<Func<T, object?>>[] includes) where T : class;
        /// <summary>
        /// Возвращает коллекцию сущностей. Для конвертации используйте .ToObservable()/ToList()/ToArray().
        /// Пример: await GetAllAsync<Student>().ToObservable();
        /// </summary>
        Task<IEnumerable<T>> GetAllWithThenIncludeAsync<T>(params Func<IQueryable<T>, IQueryable<T>>[] includes) where T : class;


       Task<T?> GetByIdAsync<T>(ulong id) where T : BaseEntity;

        Task<T> AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> entites) where T : class;

        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task DeleteRangeAsync<T>(IEnumerable<T> entity) where T : BaseEntity;

        Task<T> UpdateAsync<T>(T entity) where T : class;

        bool HasNavigationProperty<T>(T entity, string navigationPropertyName) where T : class;
        bool HasNavigationProperty<T>(T entity, string[] navigationPropertyNames) where T : class;

        Task<T?> Find<T>(T entity) where T : BaseEntity;
        EntityEntry<T> GetEntry<T>(T entity) where T : class;


        Task SaveChangesAsync();
    }
}
