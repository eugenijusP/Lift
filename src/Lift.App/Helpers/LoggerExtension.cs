using Lift.Infra.Services.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lift.App.Helpers
{
    public static class LoggerExtension
    {

        public static void ConfigureLogger(this IServiceCollection services)
        {

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<AppLogs>>();
            services.AddSingleton(typeof(ILogger), logger);
        }
    }
}
