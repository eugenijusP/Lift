using System;

namespace Lift.App.DTO {
    public record LiftLogDto {
        public DateTime ActionTime { get; set; }
        public int LiftId { get; set; }
        public int Flour { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
    }
}
