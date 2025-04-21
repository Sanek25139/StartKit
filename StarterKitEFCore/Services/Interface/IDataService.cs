
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarterKitEFCore.Model;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace StarterKitEFCore.Services.Interface
{
    public interface IDataService
    {
        IQueryable<T> GetAll<T>() where T : BaseEntity;
        Task<ObservableCollection<T>> GetAllAsync<T>() where T : BaseEntity;
        Task<ObservableCollection<T>> GetAllAsync<T>(Func<IQueryable<T>, IQueryable<T>>? queryBuilder = null) where T : BaseEntity;

        Task<ObservableCollection<T>> GetAllWithIncludeAsync<T>(params Expression<Func<T, object>>[] includes) where T : BaseEntity;
        Task<ObservableCollection<T>> GetAllWithThenIncludeAsync<T>(params Func<IQueryable<T>, IQueryable<T>>[] includes) where T : BaseEntity;


       Task<T?> GetByIdAsync<T>(ulong id) where T : BaseEntity;

        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task AddRangeAsync<T>(IEnumerable<T> entites) where T : BaseEntity;
        
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task DeleteRangeAsync<T>(IEnumerable<T> entity) where T : BaseEntity;

        Task<T> UpdateAsync<T>(T entity) where T : BaseEntity;

        bool HasNavigationProperty<T>(T entity, string navigationPropertyName) where T : BaseEntity;
        bool HasNavigationProperty<T>(T entity, string[] navigationPropertyNames) where T : BaseEntity;

        Task<T?> Find<T>(T entity) where T : BaseEntity;
        EntityEntry<T> GetEntry<T>(T entity) where T : BaseEntity;


        Task SaveChangesAsync();
    }
}
