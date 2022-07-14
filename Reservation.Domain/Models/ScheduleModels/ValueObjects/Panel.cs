using System;
namespace Reservation.Domain.Models.ScheduleModels.ValueObjects
{
    public record Panel(DateTime Date, DateTime StartTime, DateTime EndTime, string Doctor)
    {
    }
}

