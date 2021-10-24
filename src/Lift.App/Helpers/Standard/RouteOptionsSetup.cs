using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Lift.App.Helpers.Standard
{
    public sealed class RouteOptionsSetup : IConfigureOptions<RouteOptions>
    {

        public void Configure(RouteOptions options)
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        }
    }
}
