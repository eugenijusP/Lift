using System;

namespace Lift.Domain.Models {
    public sealed class LiftCall {
        public LiftCall(DateTime actionTime, int liftId, int flour, bool active) {
            this.ActionTime = actionTime;
            this.LiftId = liftId;
            this.Flour = flour;
            this.Active = active;
        }

        public DateTime ActionTime { get; }
        public int LiftId { get; }
        public int Flour { get; }
        public bool Active { get; private set; }

        public void CallProcessed() => this.Active = false;
    }
}
