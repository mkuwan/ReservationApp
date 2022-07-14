using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation.Domain.Models.ScheduleModels.Enums;
using Reservation.SharedLibrary;
using Reservation.Domain.Models.ScheduleModels;

namespace Reservation.Infrastructure.Models.ModelDTO
{
    internal static class ScheduleDTO
    {
        public static ScheduleModel FromDBtoDomainScheduleAsync(Schedule schedule)
        {
            return new ScheduleModel(
                scheduleId: schedule.ScheduleId,
                schedule.ScheduleDate,
                schedule.StartTime,
                schedule.EndTime,
                clientId: schedule.PanelStatus?.Client?.ClientId,
                managerId: schedule.PanelStatus?.Manager?.ManagerId,
                doctorId: null,
                panelStatus: schedule.PanelStatus == null ? null : schedule.PanelStatus.Status.GetEnumValueFromInt<PanelStatusType>()
                );
        }
    }
}
