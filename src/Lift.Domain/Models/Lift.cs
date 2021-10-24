using Lift.Domain.Helpers;

namespace Lift.Domain.Models {
    public sealed class LiftModel {
        public LiftModel(int id, int flour, int status) {
            this.Id = id;
            this.Flour = flour;
            this.Status = status;
        }

        public int Id { get; }
        public int Flour { get; private set; }
        public int Status { get; private set; }

        //public List<>

        public void SetFlour(int flour) => this.Flour = flour;
        public void SetStatus(LiftStatus status) => this.Status = (int)status;
    }
}
