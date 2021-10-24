using Lift.Domain.Helpers;
using Lift.Infra.Helpers.ExceptionClasses;
using Lift.Test.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Lift.Test.Repo {
    public class RepositoryTest : IClassFixture<LiftFixture> {
        private readonly LiftFixture liftFixture;

        public RepositoryTest(LiftFixture liftFixture)
            => this.liftFixture = liftFixture;


        [Fact]
        [Trait("Controlers", "LiftMove")]
        public void Test0MoveErrors() =>
            Assert.Throws<SqlInfraException>(() => this.liftFixture.liftController.CheckIfMoveAllowed(null, 1, 5));

        [Fact]
        [Trait("Repositories", "Building")]
        public void Test1Building() {

            this.liftFixture.buildingRepo.SetDefaultConfig();
            var building = this.liftFixture.buildingRepo.GetBuildingConfig();
            Assert.NotNull(building);
            Assert.Equal(15, building.FlourCount);
            Assert.Equal(4, building.LiftCount);

            this.liftFixture.buildingRepo.SetBuildingConfig(6, 10);
            building = this.liftFixture.buildingRepo.GetBuildingConfig();
            Assert.NotNull(building);
            Assert.Equal(10, building.FlourCount);
            Assert.Equal(6, building.LiftCount);

            var liftCount = this.liftFixture.dbContext.Lifts.Count();
            Assert.Equal(6, liftCount);
        }

        [Fact]
        [Trait("Repositories", "Lift")]
        public void Test2Lift() {

            this.liftFixture.liftRepo.AddLift(1);

            var lift = this.liftFixture.liftRepo.GetLift(10);
            Assert.Null(lift);

            lift = this.liftFixture.liftRepo.GetLift(1);
            Assert.NotNull(lift);
            Assert.Equal(1, lift.Id);
            Assert.Equal(1, lift.Flour);
            Assert.Equal(0, lift.Status);
        }

        [Fact]
        [Trait("Repositories", "Call")]
        public async Task Test2LiftCall() {

            this.liftFixture.liftCallRepo.AddCall(1, 5);
            this.liftFixture.liftCallRepo.AddCall(1, 7);
            this.liftFixture.dbContext.Save();

            var calls = await this.liftFixture.liftCallRepo.GetLiftCalls(1);
            Assert.Equal(2, calls.Count);

            var nextCall = await this.liftFixture.liftCallRepo.GetNextCall(1);
            Assert.NotNull(nextCall);
            nextCall.CallProcessed();
            this.liftFixture.dbContext.Save();

            calls = await this.liftFixture.liftCallRepo.GetLiftCallsNotProccesed(1);
            Assert.Single(calls);

            nextCall = await this.liftFixture.liftCallRepo.GetNextCall(1);
            Assert.NotNull(nextCall);
            nextCall.CallProcessed();
            this.liftFixture.dbContext.Save();

            calls = await this.liftFixture.liftCallRepo.GetLiftCallsNotProccesed(1);
            Assert.Empty(calls);
        }

        [Fact]
        [Trait("Repositories", "Log")]
        public async Task Test3LiftLog() {

            this.liftFixture.liftLogRepo.AddLiftLog(1, 5, LiftLogStatus.Called);
            this.liftFixture.liftLogRepo.AddLiftLog(1, 5, LiftLogStatus.DoorsOpened);            
            this.liftFixture.dbContext.Save();

            var logs = await this.liftFixture.liftLogRepo.GetLiftLog(1);
            Assert.Equal(2, logs.Count);
        }

        [Fact]
        [Trait("Controlers", "LiftMove")]
        public void Test4MoveErrors() {
            var building = this.liftFixture.buildingRepo.GetBuildingConfig();
            Assert.Throws<SqlInfraException>(() => this.liftFixture.liftController.CheckIfMoveAllowed(null, -1, 5));
            Assert.Throws<SqlInfraException>(() => this.liftFixture.liftController.CheckIfMoveAllowed(null, 2, 20));
        }

        [Fact]
        [Trait("Services", "LiftMove")]
        public async Task Test5MoveLift() {

            await this.liftFixture.liftMove.MoveLift(2, 9);

            var lift = this.liftFixture.liftRepo.GetLift(2);
            Assert.Equal(9, lift.Flour);
            Assert.Equal(0, lift.Status);

            var logMovedCount = this.liftFixture.dbContext.LiftLog.Where(s => s.LiftId == 2 && s.Status == (int)LiftLogStatus.Moved)
                                                              .Count();
            Assert.Equal(8, logMovedCount);
            var logOpenedCount = this.liftFixture.dbContext.LiftLog.Where(s => s.LiftId == 2 && s.Status == (int)LiftLogStatus.DoorsOpened)
                                                                   .Count();
            Assert.Equal(1, logOpenedCount);
            var logClosedCount = this.liftFixture.dbContext.LiftLog.Where(s => s.LiftId == 2 && s.Status == (int)LiftLogStatus.DoorsClosed)
                                                                   .Count();
            Assert.Equal(1, logClosedCount);

            await this.liftFixture.liftMove.MoveLift(2, 5);
            lift = this.liftFixture.liftRepo.GetLift(2);
        }

    }
}
