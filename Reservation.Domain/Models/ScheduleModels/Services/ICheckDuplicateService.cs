using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Models.ScheduleModels.Services
{
    public interface ICheckDuplicateService
    {
        bool ChedkDuplicate(DateOnly date, string scheduleId);
    }
}
