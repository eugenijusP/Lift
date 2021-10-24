using System;

namespace Lift.Domain.Models {
    public sealed class LiftLog {
        public LiftLog(DateTime actionTime, int liftId, int flour, int status) {
            this.ActionTime = actionTime;
            this.LiftId = liftId;
            this.Flour = flour;
            this.Status = status;
        }

        public DateTime ActionTime { get; }
        public int LiftId { get; }
        public int Flour { get; }
        public int Status { get; }
    }
}
