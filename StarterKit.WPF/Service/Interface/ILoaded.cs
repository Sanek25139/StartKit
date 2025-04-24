using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.WPF.Service.Interface
{
    public interface ILoadedAsync
    {
        Task Loaded();
    }
    public interface ILoadedAsync<T>
    {
        Task Loaded(T entity);
    }
    public interface ILoaded
    {
        void Loaded();
    }
    public interface ILoaded<T>
    {
        void Loaded(T entity);
    }

}
