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
            foreach(T value in values)
                method(value);
        }
        public static void Executes(Action method, int repeat)
        {
            for(int i = 0; i < repeat; i++)
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
        public static ICollection<T> Executes<T>(this int repeat, Func<T> method)
        {
            ICollection<T> collection = [];
            for (int i = 0; i < repeat; i++)
            {
                collection.Add(method.Invoke());
            }
            return collection;
        }

        public static ICollection<TResult> Executes<T,TResult>(this IEnumerable<T> values, Func<T,TResult> method)
        {
            ICollection<TResult> result = [];
            foreach (T value in values)
                result.Add(method(value));
            return result;
        }
        public static async Task<ICollection<TResult> > ExecutesAsync<T, TResult>(this IEnumerable<T> values, Func<T, Task<TResult>> method)
        {
            ICollection<TResult> result = [];
            foreach (T value in values)
                result.Add(await method(value));
            return result;
        }

       
    }
}
