using Microsoft.Extensions.DependencyInjection;
using MMT.DataAcces.Repository;
using MMT.Service.Configuration;

namespace MMT.Service
{
    public static class RegisterServices
    {
        public static IServiceCollection ConfigureDataService<TServiceConfiguration>(this IServiceCollection services)
            where TServiceConfiguration : class, IDataConfiguration
        {
            services.AddSingleton<IDataConfiguration, TServiceConfiguration>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();

            return services;
        }

    }
}