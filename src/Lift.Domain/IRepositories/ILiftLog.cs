using Lift.Domain.Helpers;
using Lift.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lift.Domain.IRepositories {
    public interface ILiftLog {
        Task<List<LiftLog>> GetLiftLog(int liftId);

        void AddLiftLog(int liftId, int flour, LiftLogStatus status);
    }
}
