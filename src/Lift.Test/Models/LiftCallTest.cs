using Lift.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lift.Test.Models {
    public class LiftCallTest {

        [Fact]
        [Trait("Models", "LiftCall")]
        public void LiftCallModelTest() {

            var date = DateTime.Now;
            var call = new LiftCall(date, 2, 5, true);

            Assert.Equal(date, call.ActionTime);
            Assert.Equal(5, call.Flour);
            Assert.Equal(2, call.LiftId);
            Assert.True(call.Active);

            call.CallProcessed();
            Assert.False(call.Active);
        }
    }
}
