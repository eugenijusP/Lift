using System.ComponentModel;

namespace Lift.Domain.Helpers {
    public enum LiftStatus {
        [Description("Stoped")]
        Stoped = 0,
        [Description("Moving")]
        Moving = 1,
        [Description("OpeningDoors")]
        OpeningDoors = 2,
        [Description("ClosingDoors")]
        ClosingDoors = 3,
    }
}
