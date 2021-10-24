using DVieta.Controllers;
using Lift.Infra.Helpers;
using Lift.Infra.Helpers.DB;
using Lift.Infra.Repositories;
using Lift.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Lift.Test.Data {
    public class LiftFixture : IDisposable {

        public LiftDbContext dbContext;
        public LiftRepo liftRepo;
        public BuildingRepo buildingRepo;
        public LiftCallRepo liftCallRepo;
        public LiftLogRepo liftLogRepo;

        public LiftController liftController;

        public LiftMove liftMove;
        public LiftFixture() {

            var optionsBuilder = new DbContextOptionsBuilder<LiftDbContext>();
            optionsBuilder.UseInMemoryDatabase("LiftManagmentTest");
            this.dbContext = new LiftDbContext(optionsBuilder.Options);

            this.liftRepo = new LiftRepo(this.dbContext);

            var infraModel = new InfrastructureModel() {
                DefaultBuildingFlourCount = 15,
                DefaultBuildingLiftCount = 4,
                MoveSpeed = 1,
                DoorSpeed = 2
            };
            var options = Options.Create<InfrastructureModel>(infraModel);
            this.buildingRepo = new BuildingRepo(this.dbContext, options, this.liftRepo);

            this.liftCallRepo = new LiftCallRepo(this.dbContext);
            this.liftLogRepo = new LiftLogRepo(this.dbContext);

            this.liftController = new LiftController(this.buildingRepo,
                                                     this.liftRepo,
                                                     this.liftCallRepo,
                                                     this.liftLogRepo);

            this.liftMove = new LiftMove(this.dbContext,
                                         options,
                                         this.liftCallRepo,
                                         this.liftLogRepo,
                                         this.liftRepo);
        }

        public void Dispose() => this.dbContext.Dispose();
    }
}
