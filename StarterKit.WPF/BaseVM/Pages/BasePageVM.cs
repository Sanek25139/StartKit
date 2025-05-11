using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarterKit.EF.Services.Interface;
using StarterKit.WPF.Service.Interface;
using System.Windows.Controls;

namespace StarterKit.WPF.BaseVM.Pages
{
    public abstract partial class BasePageVM(IDataService dataService) : ObservableObject, ILoadedAsync
    {
        public TabItem Tab { get; set; } = null!;

        [ObservableProperty] private bool _isLoaded;
        [ObservableProperty] private string _title = "Загрузка...";

        [RelayCommand]
        public abstract Task Loaded();

        protected IDataService DataService { get; set; } = dataService;
    }
}
