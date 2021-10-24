using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Lift.App.Helpers.Standard
{
    public sealed class JsonOptionsSetup : IConfigureOptions<JsonOptions>
    {

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.IgnoreNullValues = true;
        }
    }
}
