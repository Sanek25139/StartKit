using StarterKit.EF.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Tool
{
    public static class TypeTool
    {
        public static void Executes<T>(this IEnumerable<T> values, Action<T> method)
        {
            foreach (T value in values)
                method(value);
        }

        public static void Executes(Action method, int repeat)
        {
            for (int i = 0; i < repeat; i++)
            {
                method?.Invoke();
            }
        }

        public static ICollection<T> Executes<T>(Func<T> method, int repeat)
        {
            ICollection<T> collection = [];
            for (int i = 0; i < repeat; i++)
            {
                collection.Add(method.Invoke());
            }
            return collection;
        }

        public static void Executes(this int repeat, Action method)
        {
            for (int i = 0; i < repeat; i++)
            {
                method?.Invoke();
            }
        }

        public static async Task ExecutesAsync<T>(this IEnumerable<T> values, Func<T, Task> method)
        {
            foreach (T value in values)
                await method.Invoke(value);
        }

        public static ICollection<TResult> Executes<T, TResult>(this IEnumerable<T> values, Func<T, TResult> method)
        {
            ICollection<TResult> result = [];
            foreach (T value in values)
                result.Add(method(value));
            return result;
        }

        public static ICollection<T> Executes<T>(this int repeat, Func<T> method)
        {
            ICollection<T> collection = [];
            for (int i = 0; i < repeat; i++)
            {
                collection.Add(method.Invoke());
            }
            return collection;
        }


        public static async Task<ICollection<TResult>> ExecutesAsync<T, TResult>(this IEnumerable<T> values, Func<T, Task<TResult>> method)
        {
            ICollection<TResult> result = [];
            foreach (T value in values)
                result.Add(await method(value));
            return result;
        }


        public static async Task<IEnumerable<T>> SearchAsync<T>(this Task<IEnumerable<T>> source, Func<T, string> property, string searchText) where T : BaseEntity
        {
            IEnumerable<T> records = await source;
            ArgumentNullException.ThrowIfNull(records);
            ArgumentNullException.ThrowIfNull(searchText);

            if (string.IsNullOrWhiteSpace(searchText))
                return [.. records];

            var result = records.Where(record =>
            {

                var propertyValue = property(record);
                return propertyValue != null && propertyValue.Contains(searchText, StringComparison.CurrentCultureIgnoreCase);
            });
            return [.. result];
        }

        public static async Task<IEnumerable<T>> SearchAsync<T>(this Task<IEnumerable<T>> source, string searchText) where T : NameEntity
        {
            IEnumerable<T> records = await source;
            ArgumentNullException.ThrowIfNull(records);
            ArgumentNullException.ThrowIfNull(searchText);

            if (string.IsNullOrWhiteSpace(searchText))
                return [.. records];
            return [.. records.Where(record => record.Name != null && record.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase))];
        }
        public static async Task<IEnumerable<T>> SearchByFSMAsync<T>(
        this Task<IEnumerable<T>> source,
        string searchText)
        where T : BaseEntity, IFSM
        {
            IEnumerable<T> records = await source;
            ArgumentNullException.ThrowIfNull(records);

            if (string.IsNullOrWhiteSpace(searchText))
                return [.. records];

            return [.. records.Where(r =>
                    r.FSM != null &&
                    r.FSM.Contains(searchText))];
        }

        public static async Task<TResult> LinqAsync<T, TResult>(this Task<IEnumerable<T>> task, Func<IEnumerable<T>, TResult> request)
        {
            IEnumerable<T> source = await task;
            return request(source);
        }
        public static async Task<ObservableCollection<T>> ToObservable<T>(this Task<IEnumerable<T>> sourse) where T : class
        {
            return [.. await sourse];
        }
        public static async Task<List<T>> ToList<T>(this Task<IEnumerable<T>> sourse) where T : class
        {
            return [.. await sourse];
        }
        public static async Task<T[]> ToArray<T>(this Task<IEnumerable<T>> sourse) where T : class
        {
            return [.. await sourse];
        }
    }
}
