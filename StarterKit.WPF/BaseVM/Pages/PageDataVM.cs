using CommunityToolkit.Mvvm.ComponentModel;
using StarterKit.EF.Services.Interface;
using System.Collections.ObjectModel;

namespace StarterKit.WPF.BaseVM.Pages
{
    public abstract partial class PageDataVM<T>(IDataService dataService) : BasePageVM(dataService) where T : class
    {
        [ObservableProperty] private ObservableCollection<T> _data = [];
        [ObservableProperty] private T? _selectedData;
    }
}
