using Lift.Infra.Helpers;
using Lift.Infra.Helpers.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lift.App.Helpers
{
    public static class InfrastructureExtensions
    {

        public static void ConfigureInfrustructure(
            this IServiceCollection services,
            IConfiguration configuration) {
            services.Configure<InfrastructureModel>(configuration.GetSection("Infrastructure"));
            services.AddDbContext<LiftDbContext>(opt => opt.UseInMemoryDatabase("LiftManagment"));
        }

    }
}
