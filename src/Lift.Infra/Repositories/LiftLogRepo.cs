using Lift.Domain.Helpers;
using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Lift.Infra.Helpers.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lift.Infra.Repositories {
    public sealed class LiftLogRepo : ILiftLog {
        private readonly LiftDbContext dbContext;

        public LiftLogRepo(LiftDbContext context) => this.dbContext = context;

        public async Task<List<LiftLog>> GetLiftLog(int liftId) =>
            await this.dbContext.LiftLog.Where(a => a.LiftId == liftId).ToListAsync();

        public void AddLiftLog(int liftId, int flour, LiftLogStatus status) {
            var log = new LiftLog(DateTime.Now, liftId, flour, (int)status);
            this.dbContext.LiftLog.Add(log);
        }
    }
}
