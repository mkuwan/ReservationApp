using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using Reservation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        public void CreateManager(string managerName)
        {
            throw new NotImplementedException();
        }

        public async Task<Manager> GetManagerAsync(string managerId)
        {
            return new Manager(Guid.NewGuid().ToString(), "マネージャー"); 
        }
    }
}
