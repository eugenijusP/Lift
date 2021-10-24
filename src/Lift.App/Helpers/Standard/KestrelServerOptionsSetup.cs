using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace Lift.App.Helpers.Standard
{
    public sealed class KestrelServerOptionsSetup : IConfigureOptions<KestrelServerOptions>
    {

        public void Configure(KestrelServerOptions options)
        {
            options.AddServerHeader = false;
            options.AllowSynchronousIO = true;
        }
    }
}
