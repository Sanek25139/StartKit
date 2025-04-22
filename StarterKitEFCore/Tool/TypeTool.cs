using System;
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
            foreach(T value in values)
                method(value);
        }
        public static async Task ExecutesAsync<T>(this IEnumerable<T> values, Func<T, Task> method)
        {
            foreach (T value in values)
                await method(value);
        }
        public static ICollection<TResult> Executes<T,TResult>(this IEnumerable<T> values, Func<T,TResult> method)
        {
            ICollection<TResult> result = [];
            foreach (T value in values)
                result.Add(method(value));
            return result;
        }
        public static async Task<ICollection<TResult>> ExecutesAsync<T, TResult>(this IEnumerable<T> values, Func<T, Task<TResult>> method)
        {
            ICollection<TResult> result = [];
            foreach (T value in values)
                result.Add(await method(value));
            return result;
        }
    }
}
