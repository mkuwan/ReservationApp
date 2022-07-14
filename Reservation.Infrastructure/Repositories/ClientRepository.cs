using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using Reservation.Domain.Repositories;
using Reservation.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ReservationDbContext _context;
        public ClientRepository(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> CreateClientAsync(string clientName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 患者データ取得
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client?> GetClientAsync(string clientId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null) return null;

            return new Client(clientId, client.ClientName);
        }
    }
}
