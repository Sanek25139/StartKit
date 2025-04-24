using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarterKit.EF.Services;
using StarterKit.EF.Services.Interface;

namespace StarterKit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService<DB>(this IServiceCollection services) where DB : DbContext
        {
            services.AddTransient<IDataService, DataService<DB>>();
            // Добавьте другие сервисы по мере необходимости
            return services;
        }
        
    }
}
