using System;

namespace Lift.App.DTO {
    public record LiftCallDto {
        public DateTime ActionTime { get; set; }
        public int LiftId { get; set; }
        public int Flour { get; set; }
        public bool Active { get; set; }
    }
}
