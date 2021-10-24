using System.ComponentModel;

namespace Lift.Domain.Helpers {
    public enum LiftLogStatus {        
            [Description("Stoped")]
            Stoped = 0,
            [Description("Moved")]
            Moved = 1,
            [Description("DoorsOpened")]
            DoorsOpened = 2,
            [Description("DoursClosed")]
            DoorsClosed = 3,
            [Description("Called")]
            Called = 4
    }
}
