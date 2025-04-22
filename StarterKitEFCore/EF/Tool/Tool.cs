using StarterKit.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using StarterKit.EF.Tool;

namespace StarterKit.EF.Tool
{
    public static class Tool
    {
        public static async Task Reloads<T, TR>(this EntityEntry<T> entity, params Expression<Func<T, IEnumerable<TR>>>[] includes) where T : BaseEntity where TR : BaseEntity
        {
            foreach (var include in includes)
            {
                await entity.Collection(include).LoadAsync();
            }
        }



        public static async Task<EntityEntry<T>> TReload<T, TR>(this EntityEntry<T> entity, Expression<Func<T, IEnumerable<TR>>> include) where T : BaseEntity where TR : BaseEntity
        {
            await entity.Collection(include).LoadAsync();
            return entity;
        }
        public static async Task<EntityEntry<T>> TReload<T, TR>(this Task<EntityEntry<T>> taskEntity, Expression<Func<T, IEnumerable<TR>>> include) where T : BaseEntity where TR : BaseEntity
        {
            var entity = await taskEntity;
            return await entity.TReload(include);
            //return entity;
        }

        public static async Task<EntityEntry<T>> TReload<T, TR>(this EntityEntry<T> entity, Expression<Func<T, TR?>> include) where T : BaseEntity where TR : BaseEntity
        {
            await entity.Reference(include).LoadAsync();
            return entity;
        }

        public static async Task<EntityEntry<T>> TReload<T, TR>(this Task<EntityEntry<T>> taskEntity, Expression<Func<T,TR?>> include) where T : BaseEntity where TR : BaseEntity
        {
            var entity = await taskEntity;
            return await entity.TReload(include);

        }

        public static async Task<EntityEntry<T>> SetEntry<T,TR>(this Task<EntityEntry<T>> taskEntity, Func<T,TR> property, Func<TR,Task<EntityEntry<TR>>> reload) where T : BaseEntity where TR : BaseEntity
        {
            var entity = await taskEntity;

            // Получаем связанное свойство
            var relatedEntity = property(entity.Entity);

            // Если связанное свойство равно null, просто возвращаем текущий entity
            if (relatedEntity == null)
            {
                return entity; // Возвращаем текущий EntityEntry<T>
            }

            // Загружаем связанные данные
            var relatedEntry = entity.Context.Entry(relatedEntity);

            await reload(relatedEntity);

            return entity; // Возвращаем текущий EntityEntry<T>
        }

    }
}
