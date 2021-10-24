using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;

namespace Lift.App.Helpers.Standard
{
    public sealed class ResponseCompressionOptionsSetup : IConfigureOptions<ResponseCompressionOptions>
    {

        public void Configure(ResponseCompressionOptions options) => options.EnableForHttps = true;
    }
}
