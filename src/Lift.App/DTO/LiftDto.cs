using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lift.App.DTO {
    public record LiftDto {
        public int Id { get; set; }
        public int Flour { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }

        public List<LiftLogDto> liftLogDtos { get; set; }
        public List<LiftCallDto> liftCallDtos { get; set; }
    }

}
