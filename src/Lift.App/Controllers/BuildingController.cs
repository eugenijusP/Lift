using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DVieta.Controllers
{
    [Route("lifts/building")]
    public class BuildingController : Controller
    {
        private readonly IBuilding buildingRepo;

        public BuildingController(IBuilding buildingRepo) => this.buildingRepo = buildingRepo;

        /// <summary>
        /// Return buildyng configuration. LiftCount - number of lift in building. FlourCount - number of flours in building.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Building), 200)]        
        public IActionResult GetBuildingConfig() =>
            this.Ok(this.buildingRepo.GetBuildingConfig());

        /// <summary>
        /// Sets building configuration
        /// </summary>
        /// <param name="liftCount">Number of lift in building</param>
        /// <param name="flourCount">Number of flours in building</param>
        /// <returns></returns>
        [HttpPut]
        [Route("lifts/{liftCount}/flours/{flourCount}")]
        [ProducesResponseType(typeof(Building), 201)]
        public IActionResult SetBuildingConfig(int liftCount, int flourCount) {
            this.buildingRepo.SetBuildingConfig(liftCount, flourCount);
            return this.Created(new Uri($"{this.Request.Scheme}://{this.Request.Host}/lift/building"), null);
        }
            

    }
}
