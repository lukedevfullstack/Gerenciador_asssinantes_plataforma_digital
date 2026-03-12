using Gerenciador_asssinantes_plataforma_digital.Infrastructure.Interfaces;
using Gerenciador_asssinantes_plataforma_digital.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gerenciador_asssinantes_plataforma_digital.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            return services;
        }
    }
}