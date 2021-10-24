using Lift.Domain.Models;
using System.Threading.Tasks;

namespace Lift.Infra.Services {
    public interface ILiftMove {

        Task<Task> MoveLift(int liftId, int flour);
    }
}
