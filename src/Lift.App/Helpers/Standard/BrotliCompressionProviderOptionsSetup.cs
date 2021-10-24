using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace Lift.App.Helpers.Standard
{
    public sealed class BrotliCompressionProviderOptionsSetup : IConfigureOptions<BrotliCompressionProviderOptions>
    {

        public void Configure(BrotliCompressionProviderOptions options) => options.Level = CompressionLevel.Optimal;
    }
}
