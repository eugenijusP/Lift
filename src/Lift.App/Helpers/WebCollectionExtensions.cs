using Lift.Domain.IRepositories;
using Lift.Infra.Repositories;
using Lift.Infra.Services;
using Lift.Infra.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Lift.App.Helpers
{
    public static class WebCollectionExtensions
    {

        public static void AddWebServices(this IServiceCollection services)
        {

            /* Outside Services */
            //services.AddSingleton<ILoggerApi, LoggerApi>();

            /* Utils */
            services.AddSingleton<IMyLogger, MyLogger>();

            /* Application */
            services.AddScoped<IBuilding, BuildingRepo>();
            services.AddScoped<ILift, LiftRepo>();
            services.AddScoped<ILiftCall, LiftCallRepo>();            
            services.AddScoped<ILiftLog, LiftLogRepo>();

            services.AddScoped<ILiftMove, LiftMove>();
            
        }
    }
}
