using Lift.Domain.Helpers;
using Lift.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lift.Test.Models {
    public class BuildingTest {

        [Fact]
        [Trait("Models", "Building")]
        public void BuildingModelTest() {
            var building = new Building(3, 15);

            Assert.False(string.IsNullOrEmpty( building.Id));
            Assert.Equal(3, building.LiftCount);
            Assert.Equal(15, building.FlourCount);

            building.SetBuildingConfig(5, 10);
            Assert.Equal(5, building.LiftCount);
            Assert.Equal(10, building.FlourCount);

            Assert.Throws<DomainException>(() => building.SetBuildingConfig(1, 10));
            Assert.Throws<DomainException>(() => building.SetBuildingConfig(5, -1));

        }
    }
}
