using System.Text.Json;
using MinApi.Repository;

namespace MinApi
{
    public class DI
    {
        internal static void RegisterServices(IServiceCollection services)
        {
            //Register all services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<IMinApi, ProductsApi>();
            services.AddScoped<ProductsRepository>();
        }
    }
}