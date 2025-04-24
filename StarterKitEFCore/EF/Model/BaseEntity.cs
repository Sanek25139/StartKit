using StarterKit.EF.Attributes;
using System.Collections.Concurrent;
using System.Reflection;

namespace StarterKit.EF.Model
{
    public class BaseEntity
    {
        public ulong Id { get; set; }

        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _linkedPropertiesCache = [];

        /// <summary>
        /// Автоматически зануляет все свойства, связанные с указанным типом.
        /// </summary>
        public void SetNull(Type targetType)
        {
            var properties = _linkedPropertiesCache.GetOrAdd(
           GetType(),
           type => type.GetProperties()
               .Where(p => p.IsDefined(typeof(LinkedEntityAttribute), inherit: true))
               .ToArray()
       );

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<LinkedEntityAttribute>();
                if (attr?.TargetType == null || attr.TargetType == targetType)
                {
                    prop.SetValue(this, null);
                }
            }
        }

        /// <summary>
        /// Выполняет архивацию при удалении записи.
        /// </summary>
        public virtual void Archiving()
        {

        }
        /// <summary>
        /// Метод вызывается перед удалением 
        /// </summary>
        public virtual void OnDelete()
        {
            Archiving();
        }
        public override bool Equals(object? obj)
        {
            // Проверяем, является ли объект тем же типом
            if (obj is BaseEntity entity)
            {
                return Id == entity.Id; // Сравниваем Id
            }
            return false;
        }

        // Переопределяем метод GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Возвращаем хэш-код Id
        }

        // Перегружаем оператор ==
        public static bool operator ==(BaseEntity? left, BaseEntity? right)
        {
            // Проверяем на null и сравниваем
            if (ReferenceEquals(left, right)) return true; // Оба null или оба указывают на один объект
            if (left is null || right is null) return false; // Один из них null
            return left.Equals(right); // Сравниваем объекты
        }

        public static bool operator !=(BaseEntity? left, BaseEntity? right)
        {
            return !(left == right); // Используем перегруженный оператор ==
        }

        public static bool operator ==(BaseEntity entity, ulong id)
        {
            // Проверка на null
            if (entity is null)
            {
                return false; // Если entity null, то оно не равно id
            }
            return entity.Id == id; // Сравнение Id с переданным значением
        }

        // Перегрузка оператора !=
        public static bool operator !=(BaseEntity entity, ulong id)
        {
            return !(entity == id); // Используем перегруженный оператор ==
        }

        public static implicit operator ulong(BaseEntity entity)
        {
            // Проверка на null
            if (entity is null)
            {
                throw new InvalidOperationException("Cannot convert null BaseEntity to ulong.");
            }
            return entity.Id; // Возвращаем Id
        }

       
    }
}
