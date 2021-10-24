using Lift.Domain.Helpers;
using System;

namespace Lift.Domain.Models {
    public sealed class Building {
        public Building(int liftCount, int flourCount) {

            this.Id = Guid.NewGuid().ToString("N");
            this.LiftCount = liftCount;
            this.FlourCount = flourCount;

            this.CheckConstrains();
        }

        public string Id { get; }
        public int LiftCount { get; private set; }
        public int FlourCount { get; private set; }

        public void SetBuildingConfig(int liftCount, int flourCount) {

            this.LiftCount = liftCount;
            this.FlourCount = flourCount;

            this.CheckConstrains();
        }

        private void CheckConstrains() {
            if ( this.LiftCount < 2 ) {
                throw new DomainException("Lifts count can not be less then two!", false);
            }
            if (this.FlourCount < 1) {
                throw new DomainException("Flours count can not be less then 1!", false);
            }
        } 
    }
}
