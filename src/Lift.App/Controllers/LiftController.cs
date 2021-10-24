using Lift.App.DTO;
using Lift.App.DTO.Mapper;
using Lift.Domain.IRepositories;
using Lift.Domain.Models;
using Lift.Infra.Helpers.ExceptionClasses;
using Lift.Infra.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVieta.Controllers
{
    [Route("lifts")]
    public class LiftController : Controller
    {
        private readonly IBuilding buildingRepo;
        private readonly ILift liftRepo;
        private readonly ILiftCall liftCallRepo;
        private readonly ILiftLog liftLogRepo;

        public LiftController(IBuilding buildingRepo,
                              ILift liftRepo,
                              ILiftCall liftCallRepo,
                              ILiftLog liftLogRepo) {
            this.buildingRepo = buildingRepo;
            this.liftRepo = liftRepo;
            this.liftCallRepo = liftCallRepo;
            this.liftLogRepo = liftLogRepo;
        }

        /// <summary>
        /// Building lifts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<LiftDto>), 200)]
        public async Task<IActionResult> GetLifts() {
            var liftDtos = LiftMapper.MapLifts(await this.liftRepo.GetLiftsAsync());
            return this.Ok(liftDtos);
        }

        /// <summary>
        /// Current lift information
        /// </summary>
        /// <param name="liftId">Lift number</param>
        [HttpGet]
        [Route("{liftId}")]
        [ProducesResponseType(typeof(List<LiftDto>), 200)]
        public async Task<IActionResult> GetLift(int liftId) {
            var lift = this.liftRepo.GetLift(liftId);
            if (lift is null) {
                return this.BadRequest("Lift not found");
            }
            var liftDto = LiftMapper.MapLift(lift);
            liftDto.liftCallDtos = LiftMapper.MapLiftCalls(await this.liftCallRepo.GetLiftCalls(liftId));
            liftDto.liftLogDtos = LiftMapper.MapLiftLogs(await this.liftLogRepo.GetLiftLog(liftId));

            return this.Ok(liftDto);
        }

        /// <summary>
        /// Lift call, movement, doors log
        /// </summary>
        /// <param name="liftId">Lift number</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{liftId}/log")]        
        [ProducesResponseType(typeof(List<LiftLogDto>), 200)]
        public async Task<IActionResult> GetLiftLog(int liftId) {
            var logDtos = LiftMapper.MapLiftLogs(await this.liftLogRepo.GetLiftLog(liftId));
            return this.Ok(logDtos);
        }

        /// <summary>
        /// Lift call log. Active = true, call is in queue. Active = false, call is processed
        /// </summary>
        /// <param name="liftId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{liftId}/calls")]
        [ProducesResponseType(typeof(List<LiftCallDto>), 200)]
        public async Task<IActionResult> GetLiftCalls(int liftId) {
            var callDtos = LiftMapper.MapLiftCalls(await this.liftCallRepo.GetLiftCalls(liftId));
            return this.Ok(callDtos);
        }

        /// <summary>
        /// Order lift to move to flour
        /// </summary>
        /// <param name="liftId">Lift number</param>
        /// <param name="toFlour">To witch four is lift called</param>
        /// <param name="serviceScopeFactory"></param>
        [HttpPost]
        [Route("{liftId}/flours/{toFlour}")]
        [ProducesResponseType(typeof(Building), 202)]
        public IActionResult MoveLift(
            int liftId,
            int toFlour,
            [FromServices]IServiceScopeFactory serviceScopeFactory) {

            var building = this.buildingRepo.GetBuildingConfig();
            this.CheckIfMoveAllowed(building, liftId, toFlour);

            _ = Task.Run( async () => {
                using var scope = serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<ILiftMove>();
                await repo.MoveLift(liftId, toFlour);
            });

            return this.Accepted();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void CheckIfMoveAllowed(Building building, int liftId, int toFlour) {
            if (building is null) {
                throw new SqlInfraException("Building configutation not found");
            }
            if (liftId > building.LiftCount || liftId <= 0) {
                throw new SqlInfraException("Building does not have such lift!");
            }
            if (toFlour > building.FlourCount || toFlour <= 0) {
                throw new SqlInfraException("Building does not have such flour!");
            }
        }


    }
}
