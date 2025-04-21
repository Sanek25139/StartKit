using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKitEFCore.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using StarterKitEFCore.Services;
    using StarterKitEFCore.Services.Interface;

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
