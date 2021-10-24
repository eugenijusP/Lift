using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Lift.App.Helpers.Standard
{
    public sealed class ConsoleLoggerOptionsSetup : IConfigureOptions<ConsoleLoggerOptions>
    {
        private readonly IHostEnvironment environment;

        public ConsoleLoggerOptionsSetup(IHostEnvironment environment) => this.environment = environment;

        public void Configure(ConsoleLoggerOptions options)
        {
            if (!this.environment.IsDevelopment())
            {
#pragma warning disable CS0618 // Type or member is obsolete
                options.DisableColors = true;
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }
    }
}
