using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarterKit.EF.Model;
using StarterKit.EF.Services.Interface;
using StarterKit.WPF.Service.Interface;
using System.Windows;

namespace StarterKit.WPF.BaseVM.Windows
{
    public abstract partial class BaseWindowVM(IDataService dataService) : ObservableObject
    {
        protected IDataService DataService { get; set; } = dataService;
        public Window window = null!;

        [RelayCommand]
        protected void Close()
        {
            window?.Close();
        }
        public BaseWindowVM Init(Window window)
        {
            this.window = window;
            return this;
        }
    }

    public abstract partial class WindowEditBaseVM<T>(IDataService dataService) : BaseWindowVM(dataService), ILoadedAsync<T> where T : BaseEntity
    {
        [ObservableProperty] private T? _selectedValue;

        public abstract Task Loaded(T entity);
        [RelayCommand]
        protected abstract Task Add();
        [RelayCommand]
        protected abstract Task Save();
    }

    public abstract partial class WindowCreateBase<T>(IDataService dataService) : BaseWindowVM(dataService), ILoadedAsync<T> where T : BaseEntity
    {
        public abstract Task Loaded(T entity);
        [RelayCommand]
        public abstract Task Create();
    }
}
