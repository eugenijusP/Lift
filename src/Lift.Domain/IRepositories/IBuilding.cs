using Lift.Domain.Models;

namespace Lift.Domain.IRepositories {
    public interface IBuilding {
        void SetBuildingConfig(int liftCount, int flourCount);
        void SetDefaultConfig();
        Building? GetBuildingConfig();
    }
}
