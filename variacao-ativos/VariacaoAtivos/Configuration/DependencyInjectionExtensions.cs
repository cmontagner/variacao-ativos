using VariacaoAtivos.Infra.Repositories;
using VariacaoAtivos.Infra.Repositories.Interfaces;
using VariacaoAtivos.Service.Services;
using VariacaoAtivos.Service.Services.Interfaces;

namespace VariacaoAtivos.Api.Controllers.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<IActiveVariationService, ActiveVariationService>();

            #endregion Services

            #region Repositories

            services.AddScoped<IActiveVariationRepository, ActiveVariationRepository>();

            #endregion Repositories
        }

        public static void AddOptions(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();
        }
    }
}