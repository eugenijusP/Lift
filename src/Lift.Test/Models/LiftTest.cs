using Lift.Domain.Helpers;
using Lift.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lift.Test.Models {
    public class LiftTest {

        [Fact]
        [Trait("Models", "Lift")]
        public void LiftModelTest() {
            var lift = new LiftModel(1, 2, (int)LiftStatus.Stoped);

            Assert.Equal(1, lift.Id);
            Assert.Equal(2, lift.Flour);
            Assert.Equal(0, lift.Status);

            lift.SetFlour(5);
            Assert.Equal(5, lift.Flour);

            lift.SetStatus(LiftStatus.Moving);
            Assert.Equal(1, lift.Status);

            lift.SetStatus(LiftStatus.OpeningDoors);
            Assert.Equal(2, lift.Status);

            lift.SetStatus(LiftStatus.ClosingDoors);
            Assert.Equal(3, lift.Status);
        }
    }
}
