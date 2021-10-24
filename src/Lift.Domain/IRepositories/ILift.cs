using Lift.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lift.Domain.IRepositories {
    public interface ILift {
        LiftModel? GetLift(int liftId);

        Task<List<LiftModel>> GetLiftsAsync();

        void AddLift(int liftId);                
    }
}
