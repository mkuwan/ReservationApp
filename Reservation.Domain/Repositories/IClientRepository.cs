using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetClientAsync(string clientId);

        Task<Client?> CreateClientAsync(string clientName);
    }
}
