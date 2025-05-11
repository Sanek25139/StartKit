using Microsoft.Extensions.DependencyInjection;
using StarterKit.EF.Model;
using StarterKit.WPF.Interface;
using StarterKit.WPF.Service.Interface;
using System.Windows;
using static StarterKit.WPF.Interface.ICallback;

namespace StarterKit.WPF.Service
{
    public class WindowService(IServiceProvider serviceProvider, Dictionary<Type, Type> windows) : ITypeControlService
    {
        IServiceProvider ServiceProvider { get; set; } = serviceProvider;

        private readonly Dictionary<Type, Type> ViewModelToWindowMap = windows;

        public void ShowControl(Type viewModel)
        {
            Window window = GetWindow(viewModel);
            window.Show();
        }

        public void ShowControl(Type viewModel, Action callbeck)
        {
            Window window = GetWindow(viewModel);
            if (window is ICallback windowCallbeck)
            {
                windowCallbeck.Subscribe(callbeck);
            }
            window.Show();
        }

        public void ShowControl<T>(Type viewModel, Action<T> callbeck) where T : BaseEntity
        {
            Window window = GetWindow(viewModel);
            if (window is ICallback<T> windowCallbeck)
            {
                windowCallbeck.Subscribe(callbeck);
            }
            window.Show();
        }

        public async Task ShowControlAsync<T>(Type viewModel, Func<Task> callback, T? entity) where T : BaseEntity
        {
            Window window = GetWindow(viewModel);
            if (window is ICallbackAsync windowCallback)
            {
                windowCallback.Subscribe(callback);
                if (entity != null && windowCallback.ObservableObject is ILoadedAsync<T> vm)
                {
                    await vm.Loaded(entity);
                }
                else if (entity == null && windowCallback.ObservableObject is ILoadedAsync vm2)
                {
                    await vm2.Loaded();
                }
            }
            await Application.Current.Dispatcher.InvokeAsync(() => window.Show());
        }

        public async Task ShowControlAsync(Type viewModel, Func<Task> callback)
        {
            Window window = GetWindow(viewModel);
            if (window is ICallbackAsync windowCallback)
            {
                windowCallback.Subscribe(callback);
                if (windowCallback.ObservableObject is ILoadedAsync vm)
                {
                    await vm.Loaded();
                }
            }
            await Application.Current.Dispatcher.InvokeAsync(() => window.Show());
        }

        public async Task ShowControlAsync<T>(Type viewModel, Func<T, Task> callback) where T : BaseEntity
        {
            Window window = GetWindow(viewModel)!;

            if (window is ICallbackAsync<T> windowCallback)
            {
                if (windowCallback.ObservableObject is ILoadedAsync vm)
                {
                    await vm.Loaded();
                }
                windowCallback.Subscribe(callback);
            }
            await Application.Current.Dispatcher.InvokeAsync(() => window.Show());
        }

        private Window GetWindow(Type viewModel)
        {
            if (ViewModelToWindowMap.TryGetValue(viewModel, out Type? vm))
            {
                var windowType = vm;
                return (Window)ServiceProvider.GetService(windowType)!;
            }
            else if (ServiceProvider.GetRequiredService(viewModel) is Window window)
            {
                return window;
            }
            else
            {
                throw new ArgumentNullException(nameof(viewModel), "Argument cannot be null.");
            }
        }
    }
}
