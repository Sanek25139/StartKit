using StarterKit.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.WPF.Service.Interface
{
    public interface ITypeControlService
    {
        void ShowControl(Type viewModel);

        void ShowControl(Type viewModel, Action callbeck);
        void ShowControl<T>(Type viewModel, Action<T> callbeck) where T : BaseEntity;

        Task ShowControlAsync(Type viewModel, Func<Task> callback);
        Task ShowControlAsync<T>(Type viewModel, Func<Task> callback, T? entity) where T : BaseEntity;
        Task ShowControlAsync<T>(Type viewModel, Func<T, Task> callback) where T : BaseEntity;
    }
}
