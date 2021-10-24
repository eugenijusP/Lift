using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lift.App.Helpers.Standard
{
    public sealed class ApiBehaviorOptionsSetup : IConfigureOptions<ApiBehaviorOptions>
    {

        public void Configure(ApiBehaviorOptions options) => options.SuppressMapClientErrors = true;
    }
}
