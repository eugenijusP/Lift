using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace Lift.App.Helpers.Standard
{
    public sealed class GzipCompressionProviderOptionsSetup : IConfigureOptions<GzipCompressionProviderOptions>
    {

        public void Configure(GzipCompressionProviderOptions options) => options.Level = CompressionLevel.Optimal;
    }
}
