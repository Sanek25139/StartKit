using Microsoft.Extensions.DependencyInjection;
using StarterKit.EF.Model;
using StarterKit.WPF.BaseVM.Windows;
using StarterKit.WPF.Interface;
using StarterKit.WPF.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StarterKit.WPF.Service
{
    public class WindowSelectionService(IServiceProvider serviceProvider)
    {
        private readonly IServiceProvider ServiceProvider = serviceProvider;

        public async Task ShowAsync<T,TR>(Func<TR,Task> callback) where T : Window, ICallbackAsync<TR> where TR : BaseEntity
        {
            T window = GetWindow<T>()!;

            if (window.ObservableObject is ILoadedAsync vm)
            {
                await vm.Loaded();
            }
            window.Subscribe(callback);

            await Application.Current.Dispatcher.InvokeAsync(() => window.Show());
        }

        public async Task ShowAsync<T, TR>(Func<IQueryable<TR>, IQueryable<TR>>? queryBuilder, Func<TR, Task> callback) where T : Window, ICallbackAsync<TR> where TR : BaseEntity
        {
            T window = GetWindow<T>()!;
            if (window.ObservableObject is WindowSelectionBaseVM<TR> vm)
            {
                vm.QueryBuilder = queryBuilder;
                await vm.Loaded();
            }
            window.Subscribe(callback);
            await Application.Current.Dispatcher.InvokeAsync(() => window.Show());
        }


        private T GetWindow<T>() where T : Window
        {
            if (ServiceProvider.GetRequiredService<T>() is T window)
            {
                return window;
            }
            else
            {
                throw new ArgumentNullException(nameof(T), "Argument cannot be null.");
            }
        }
    }
}
