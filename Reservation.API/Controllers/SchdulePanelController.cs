using Microsoft.AspNetCore.Mvc;
using Reservation.Application.IServices;
using Reservation.Application.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SchdulePanelController : ControllerBase
    {
        private readonly ISchedulerService _scheduler;

        public SchdulePanelController(ISchedulerService scheduler)
        {
            _scheduler = scheduler;
        }



        // GET: api/<SchdulePanelController>
        [HttpGet("{fromDate}/{toDate}")]
        public async Task<IActionResult> GetSchedules(DateTime fromDate, DateTime toDate)
        {
            return await _scheduler.GetSchedulesAsync(fromDate, toDate);
        }

        // GET api/<SchdulePanelController>/5
        [HttpGet("{scheduleId}")]
        public async Task<IActionResult> GetSchedule(string scheduleId)
        {
            return await _scheduler.GetPanelAsync(scheduleId);
        }

        [HttpPost("reserve/{clientId}")]
        public async Task<IActionResult> TakeReservation(string clientId, [FromBody] ScheduleForClientViewModel schedule)
        {
            return await _scheduler.TakeReservationAsync(schedule.ScheduleId, clientId);
        }

        [HttpPost("cancel/{clientId}")]
        public async Task<IActionResult> cancelReservation(string clientId, [FromBody] ScheduleForClientViewModel value)
        {
            return await _scheduler.CancelReservationAsync(value.ScheduleId, clientId);
        }

        [HttpPost("approve/{managerId}")]
        public async Task<IActionResult> approveReservation(string managerId, [FromBody] ScheduleForClientViewModel value)
        {
            return await _scheduler.ApproveReservationAsync(value.ScheduleId, managerId);
        }

        [HttpPost("notapprove/{managerId}")]
        public async Task<IActionResult> notApproveReservation(string managerId, [FromBody] ScheduleForClientViewModel value)
        {
            return await _scheduler.NotApproveReservationAsync(value.ScheduleId, managerId);
        }

        //// PUT api/<SchdulePanelController>/5
        //[HttpPut]
        //public async Task<IActionResult> TakeReservation([FromBody] ScheduleForClientViewModel value)
        //{
        //}

        //// DELETE api/<SchdulePanelController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
