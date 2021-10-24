using Lift.Domain.Helpers;
using Lift.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lift.Test.Models {
    public class LiftLogTest {

        [Fact]
        [Trait("Models", "LiftLog")]
        public void LiftLogModelTest() {
            var date = DateTime.Now;
            var log = new LiftLog(date, 1, 7, (int)LiftLogStatus.Called);

            Assert.Equal(date, log.ActionTime);
            Assert.Equal(1, log.LiftId);
            Assert.Equal(7, log.Flour);
            Assert.Equal(4, log.Status);

            log = new LiftLog(date, 1, 7, (int)LiftLogStatus.Stoped);
            Assert.Equal(0, log.Status);

            log = new LiftLog(date, 1, 7, (int)LiftLogStatus.Moved);
            Assert.Equal(1, log.Status);

            log = new LiftLog(date, 1, 7, (int)LiftLogStatus.DoorsOpened);
            Assert.Equal(2, log.Status);

            log = new LiftLog(date, 1, 7, (int)LiftLogStatus.DoorsClosed);
            Assert.Equal(3, log.Status);
        }
    }
}
