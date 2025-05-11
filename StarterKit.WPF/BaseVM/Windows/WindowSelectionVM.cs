using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarterKit.EF.Model;
using StarterKit.EF.Services.Interface;
using StarterKit.WPF.Service.Interface;
using System.Collections.ObjectModel;
using StarterKit.Tool;

namespace StarterKit.WPF.BaseVM.Windows
{
    public partial class WindowSelectionVM<T>(IDataService dataService) : WindowSelectionBaseVM<T>(dataService), ILoadedAsync where T : NameEntity
    {
        public override async Task Searh(string value)
        {
            Data = await DataService.GetAllAsync<T>().SearchAsync<T, ObservableCollection<T>>(value);
        }
    }
    public abstract partial class WindowSelectionBaseVM<T>(IDataService dataService) : BaseWindowVM(dataService), ILoadedAsync where T : BaseEntity
    {
        [ObservableProperty] private T? _selectedValue;
        [ObservableProperty] private ObservableCollection<T> _data = [];

        public bool IsChoose = false;

        public virtual async Task Loaded()
        {
            Data = await DataService.GetAllAsync<T>();
        }
        [RelayCommand]
        private void Choose()
        {
            IsChoose = true;
            Close();
        }
        [RelayCommand]
        public abstract Task Searh(string value);


    }
}
