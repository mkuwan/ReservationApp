using System;
using Reservation.Domain.Models.ScheduleModels;

namespace Reservation.Domain.Repositories
{
    public interface ISchedulerRepository
    {
        Task<ScheduleModel?> GetScheduleAsync(string scheduleId);

        Task<List<ScheduleModel>> GetSchedulesAsync(DateOnly fromDate, DateOnly toDate);

        Task MakeTemporaryReservation(ScheduleModel schedule);

        Task CancelTemporaryReservation(ScheduleModel schedule);

        Task ApproveReservaton(ScheduleModel schedule);

        Task NotApproveReservaton(ScheduleModel schedule);
    }
}

