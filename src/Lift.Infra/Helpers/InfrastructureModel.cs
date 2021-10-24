
namespace Lift.Infra.Helpers
{
    public record InfrastructureModel
    {
        public int DefaultBuildingLiftCount { get; set; } 
        public int DefaultBuildingFlourCount { get; set; }
        public int MoveSpeed { get; set; }
        public int DoorSpeed { get; set; }
    }
}
