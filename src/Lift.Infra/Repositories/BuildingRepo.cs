using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Lift.Infra.Helpers;
using Lift.Infra.Helpers.DB;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Lift.Infra.Repositories {
    public sealed class BuildingRepo : IBuilding {
        private readonly LiftDbContext dbContext;
        private readonly IOptions<InfrastructureModel> options;
        private readonly ILift liftRepo;

        public BuildingRepo(LiftDbContext context,
                            IOptions<InfrastructureModel> options,
                            ILift liftRepo) {
            this.dbContext = context;
            this.options = options;
            this.liftRepo = liftRepo;
        }

        public Building? GetBuildingConfig() => this.dbContext.Buildings.FirstOrDefault();

        public void SetDefaultConfig() {
            this.SetBuildingConfig(this.options.Value.DefaultBuildingLiftCount,
                                   this.options.Value.DefaultBuildingFlourCount);
            this.SetUpLifts(this.options.Value.DefaultBuildingLiftCount);
        }

        public void SetBuildingConfig(int liftCount, int flourCount) {
            var building = this.GetBuildingConfig();
            if (building is null) {
                this.NewBuilding(liftCount, flourCount);
            }
            else {
                building.SetBuildingConfig(liftCount, flourCount);
            }
            this.SetUpLifts(liftCount);
            this.dbContext.Save();
        }        

        private void NewBuilding(int liftCount, int flourCount) {
            var building = new Building(liftCount, flourCount);
            this.dbContext.Buildings.Add(building);
        }

        private void SetUpLifts(int liftCount) {
            for (var i = 1; i <= liftCount; i++) {
                this.liftRepo.AddLift(i);
            }
        }
    }
}
