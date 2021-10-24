using Lift.Domain.Helpers;
using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Lift.Infra.Helpers;
using Lift.Infra.Helpers.DB;
using Lift.Infra.Helpers.ExceptionClasses;
using Lift.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lift.Infra.Repositories {
    public sealed class LiftRepo : ILift {
        private readonly LiftDbContext dbContext;

        public LiftRepo(LiftDbContext context) => this.dbContext = context;

        public void AddLift(int liftId) {            
            if (!this.LiftExits(liftId)) {
                this.dbContext.Lifts.Add(new LiftModel(liftId, 1, (int)LiftStatus.Stoped));
                this.dbContext.Save();
            }
        }
        public LiftModel? GetLift(int liftId) => this.dbContext.Lifts.FirstOrDefault(a => a.Id == liftId);
        public async Task<List<LiftModel>> GetLiftsAsync() => await this.dbContext.Lifts.ToListAsync();

        private bool LiftExits(int liftId) => this.dbContext.Lifts.Where(a => a.Id == liftId).Any();
    }
}
