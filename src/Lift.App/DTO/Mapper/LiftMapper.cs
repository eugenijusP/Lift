using Lift.Domain.Helpers;
using Lift.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lift.App.DTO.Mapper {
    public class LiftMapper {

        public static List<LiftLogDto> MapLiftLogs(List<LiftLog> liftLogs) {
            var logDtos = new List<LiftLogDto>();
            foreach( var log in liftLogs) {
                logDtos.Add(MapLiftLog(log));
            }
            return logDtos;
        }

        public static LiftLogDto MapLiftLog(LiftLog liftLog) =>
             new LiftLogDto() {
                 ActionTime = liftLog.ActionTime,
                 Flour = liftLog.Flour,
                 LiftId = liftLog.LiftId,
                 Status = liftLog.Status,
                 StatusText = GetLiftLogStatusText(liftLog.Status)
             };

        private static string GetLiftLogStatusText(int status) {
            var statusEnum = (LiftLogStatus)Enum.ToObject(typeof(LiftLogStatus), status);
            return statusEnum.ToString();
        }

        public static List<LiftCallDto> MapLiftCalls(List<LiftCall> liftCalls) {
            var callDtos = new List<LiftCallDto>();
            foreach (var log in liftCalls) {
                callDtos.Add(MapLiftCall(log));
            }
            return callDtos;
        }

        public static LiftCallDto MapLiftCall(LiftCall liftCall) =>
             new LiftCallDto() {
                 ActionTime = liftCall.ActionTime,
                 Flour = liftCall.Flour,
                 LiftId = liftCall.LiftId,
                 Active = liftCall.Active
             };

        public static List<LiftDto> MapLifts(List<LiftModel> lifts) {
            var liftDtos = new List<LiftDto>();
            foreach (var lift in lifts) {
                liftDtos.Add(MapLift(lift));
            }
            return liftDtos;
        }

        public static LiftDto MapLift(LiftModel lift) =>
             new LiftDto() {                 
                 Flour = lift.Flour,
                 Status = lift.Status,
                 StatusText = GetLiftStatusText(lift.Status),
                 Id = lift.Id
             };

        private static string GetLiftStatusText(int status) {
            var statusEnum = (LiftStatus)Enum.ToObject(typeof(LiftStatus), status);
            return statusEnum.ToString();
        }
    }
}
