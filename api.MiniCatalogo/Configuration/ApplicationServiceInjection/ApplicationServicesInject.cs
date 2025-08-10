using api.MiniCatalogo.Repository.Categori;
using api.MiniCatalogo.Repository.Product;
using Microsoft.Extensions.Options;

namespace api.MiniCatalogo.Configuration.ApplicationServiceInjection
{
    public static class ApplicationServicesInject
    {
        public static void AddApplicationServicesInject(this WebApplicationBuilder build)
        {
            IServiceCollection services = build.Services;

            services.Configure<Config>(build.Configuration.GetSection("Config"));

            services.AddSingleton(provider =>
                provider.GetRequiredService<IOptions<Config>>().Value);
        }
    }
}
