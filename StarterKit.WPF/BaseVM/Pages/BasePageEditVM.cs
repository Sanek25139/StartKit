using CommunityToolkit.Mvvm.Input;
using StarterKit.EF.Model;
using StarterKit.EF.Services.Interface;
using StarterKit.WPF.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.WPF.BaseVM.Pages
{
    public abstract partial class BasePageEditVM<T, TService>(IDataService dataService, TService controlService) : BasePageEditVM<T>(dataService) where T : BaseEntity where TService : ITypeControlService
    {
        protected TService ControlService => controlService;

        protected override async Task Add()
        {
            await ControlService.ShowControlAsync<T>(GetType(), Callback);

        }

        protected override async Task Change(T entity)
        {
            await ControlService.ShowControlAsync(GetType(), Callback, entity);
        }

        private async Task Callback()
        {
            SelectedData = null;
            IsLoaded = false;
            await Loaded();
        }
    }
    public abstract partial class BasePageEditVM<T>(IDataService dataService) : PageDataVM<T>(dataService) where T : BaseEntity
    {

        [RelayCommand]
        protected abstract Task Add();

        [RelayCommand]
        protected abstract Task Change(T entity);

        [RelayCommand]
        protected abstract Task Remove(IEnumerable<T> entities);
    }
}
