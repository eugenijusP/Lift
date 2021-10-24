using Lift.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lift.Domain.IRepositories {
    public interface ILiftCall {
        Task<List<LiftCall>> GetLiftCalls(int liftId);

        Task<List<LiftCall>> GetLiftCallsNotProccesed(int liftId);

        Task<LiftCall?> GetNextCall(int liftId);

        void AddCall(int liftId, int flour);
    }
}
