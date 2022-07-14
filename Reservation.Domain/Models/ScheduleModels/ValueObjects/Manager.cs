using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Models.ScheduleModels.ValueObjects
{
    public record Manager(string managerId, string managerName)
    {
    }
}
