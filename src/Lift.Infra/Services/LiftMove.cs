using Lift.Domain.Helpers;
using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Lift.Infra.Helpers;
using Lift.Infra.Helpers.DB;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Lift.Infra.Services {
    public class LiftMove : ILiftMove {
        private readonly LiftDbContext dbContext;
        private readonly ILiftCall liftCallRepo;
        private readonly ILiftLog liftLogRepo;
        private readonly ILift liftRepo;
        private readonly int moveSpeed;
        private readonly int doorSpeed;

        public LiftMove(LiftDbContext context,
                        IOptions<InfrastructureModel> options,
                        ILiftCall liftCallRepo,
                        ILiftLog liftLogRepo,
                        ILift liftRepo) {
            this.dbContext = context;
            this.liftCallRepo = liftCallRepo;
            this.liftLogRepo = liftLogRepo;
            this.liftRepo = liftRepo;
            this.moveSpeed = options.Value.MoveSpeed <= 0 ? 1 : options.Value.MoveSpeed;
            this.doorSpeed = options.Value.DoorSpeed <= 0 ? 1 : options.Value.DoorSpeed;
        }

        public async Task<Task> MoveLift(int liftId, int flour) {

            var lift = this.liftRepo.GetLift(liftId);
            if (lift is null) {
                return Task.CompletedTask;
            }

            this.CallMade(liftId, flour);
            await this.DoMove(lift);
            return Task.CompletedTask; 
        }

        private void CallMade(int liftId, int flour) {
            this.liftLogRepo.AddLiftLog(liftId, flour, LiftLogStatus.Called);
            this.liftCallRepo.AddCall(liftId, flour);
            this.dbContext.Save();
        }


        private async Task DoMove(LiftModel lift) {
            if (lift.Status != (int)LiftStatus.Stoped) {
                return;
            }
            var noMoreMoves = false;
            while (!noMoreMoves) {
                var call = await this.liftCallRepo.GetNextCall(lift.Id);
                if (call is null) {
                    this.LiftStoped(lift);
                    return;
                }

                var up = call.Flour >= lift.Flour;
                while (call.Flour != lift.Flour) {
                    this.LiftMoving(lift, up);
                }
                this.LiftOpenDoors(lift);
                this.LiftCloseDoors(lift);
                call.CallProcessed();
                this.dbContext.Save();
            }
            return;
        }

        private void LiftStoped(LiftModel lift) {
            this.liftLogRepo.AddLiftLog(lift.Id, lift.Flour, LiftLogStatus.Stoped);
            lift.SetStatus(LiftStatus.Stoped);
            this.dbContext.Save();
        }

        private void LiftOpenDoors(LiftModel lift) {
            lift.SetStatus(LiftStatus.OpeningDoors);
            this.dbContext.Save();
            Thread.Sleep(this.doorSpeed * 1000);
            this.liftLogRepo.AddLiftLog(lift.Id, lift.Flour, LiftLogStatus.DoorsOpened);
            this.dbContext.Save();
        }

        private void LiftCloseDoors(LiftModel lift) {
            lift.SetStatus(LiftStatus.ClosingDoors);
            this.dbContext.Save();
            Thread.Sleep(this.doorSpeed * 1000);
            this.liftLogRepo.AddLiftLog(lift.Id, lift.Flour, LiftLogStatus.DoorsClosed);
            this.dbContext.Save();
        }

        private void LiftMoving(LiftModel lift, bool up) {
            lift.SetStatus(LiftStatus.Moving);
            this.dbContext.Save();
            Thread.Sleep(this.moveSpeed * 1000);
            lift.SetFlour(up ? lift.Flour + 1 : lift.Flour - 1);
            this.liftLogRepo.AddLiftLog(lift.Id, lift.Flour, LiftLogStatus.Moved);
            this.dbContext.Save();
        }
    }
}
