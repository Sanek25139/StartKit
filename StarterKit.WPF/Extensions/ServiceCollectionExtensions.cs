using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;
using StarterKit.Extensions;

namespace StarterKit.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVVMTransient<TView, TViewModel>(this IServiceCollection services) where TView : FrameworkElement, IComponentConnector where TViewModel : class, INotifyPropertyChanged
        {
            services.AddTransient<TView>();
            services.AddTransient<TViewModel>();
            return services;
        }
        public static IServiceCollection AddVVMSingleton<TView, TViewModel>(this IServiceCollection services) where TView : FrameworkElement, IComponentConnector where TViewModel : class, INotifyPropertyChanged
        {
            services.AddSingleton<TView>();
            services.AddSingleton<TViewModel>();
            return services;
        }
    }
}
