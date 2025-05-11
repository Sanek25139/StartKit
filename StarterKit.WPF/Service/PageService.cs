using StarterKit.EF.Model;
using StarterKit.WPF.Interface;
using StarterKit.WPF.Service.Interface;
using System.Windows;

namespace Cinema.Services
{
    public class PageService(IPageControl pageControl, Dictionary<Type, Type> viewModelToPageMap) : ITypeControlService
    {
        private readonly Dictionary<Type, Type> ViewModelToPageMap = viewModelToPageMap;

        Window Window => pageControl as Window;
        IPageControl PageControl => pageControl;

        public void ShowControl(Type viewModel)
        {
            throw new NotImplementedException();
        }

        public void ShowControl(Type viewModel, Action callbeck)
        {
            throw new NotImplementedException();
        }

        public void ShowControl<T>(Type viewModel, Action<T> callbeck) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task ShowControlAsync<T>(Type viewModel, Func<Task> callback, T? entity = null) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task ShowControlAsync<T>(Type viewModel, Func<T, Task> callback) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}