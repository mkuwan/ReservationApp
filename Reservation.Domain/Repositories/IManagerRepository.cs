using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Repositories
{
    public interface IManagerRepository
    {
        Task<Manager> GetManagerAsync(string managerId);

        void CreateManager(string managerName);
    }
}
