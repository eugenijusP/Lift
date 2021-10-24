using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Lift.App.Helpers.Standard
{
    public static class OptionsStandardExtensions
    {

        public static void AddOptionsStandardExtensions(this IServiceCollection services)
        {

            services.AddSingleton<IConfigureOptions<ApiBehaviorOptions>, ApiBehaviorOptionsSetup>();
            services.AddSingleton<IConfigureOptions<BrotliCompressionProviderOptions>, BrotliCompressionProviderOptionsSetup>();
            services.AddSingleton<IConfigureOptions<ConsoleLoggerOptions>, ConsoleLoggerOptionsSetup>();
            services.AddSingleton<IConfigureOptions<GzipCompressionProviderOptions>, GzipCompressionProviderOptionsSetup>();
            services.AddSingleton<IConfigureOptions<JsonOptions>, JsonOptionsSetup>();
            services.AddSingleton<IConfigureOptions<KestrelServerOptions>, KestrelServerOptionsSetup>();
            services.AddSingleton<IConfigureOptions<ResponseCompressionOptions>, ResponseCompressionOptionsSetup>();
            services.AddSingleton<IConfigureOptions<RouteOptions>, RouteOptionsSetup>();
        }
    }
}
