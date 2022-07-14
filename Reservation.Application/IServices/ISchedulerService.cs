using System;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Models;

namespace Reservation.Application.IServices
{
    public interface ISchedulerService
    {
        Task<IActionResult> GetSchedulesAsync(DateTime startDate, DateTime endDate);
        //Task<ActionResult<List<ScheduleForClientViewModel>>> getSchedulesAsync(DateTime startDate, DateTime endDate);


        Task<IActionResult> GetPanelAsync(string scheduleId);

        Task<IActionResult> TakeReservationAsync(string scheduleId, string clientId);

        Task<IActionResult> CancelReservationAsync(string scheduleId, string clientId);

        Task<IActionResult> ApproveReservationAsync(string scheduleId, string managerId);

        Task<IActionResult> NotApproveReservationAsync(string scheduleId, string managerId);
    }
}

