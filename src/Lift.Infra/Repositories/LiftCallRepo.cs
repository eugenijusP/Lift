using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Lift.Infra.Helpers.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lift.Infra.Repositories {
    public sealed class LiftCallRepo : ILiftCall {
        private readonly LiftDbContext dbContext;

        public LiftCallRepo(LiftDbContext context) => this.dbContext = context;

        public void AddCall(int liftId, int flour) {
            var call = new LiftCall(DateTime.Now, liftId, flour, true);
            this.dbContext.LiftCalls.Add(call);            
        }
        public async Task<List<LiftCall>> GetLiftCalls(int liftId) =>
            await this.dbContext.LiftCalls.Where(a => a.LiftId == liftId).ToListAsync();
        public async Task<List<LiftCall>> GetLiftCallsNotProccesed(int liftId) =>
            await this.dbContext.LiftCalls.Where(a => a.LiftId == liftId && a.Active).
                                           OrderBy(o => o.ActionTime).ToListAsync();
        public async Task<LiftCall?> GetNextCall(int liftId) =>
            await this.dbContext.LiftCalls.Where(a => a.LiftId == liftId && a.Active).
                                    OrderBy(o => o.ActionTime).FirstOrDefaultAsync();
    }
}
