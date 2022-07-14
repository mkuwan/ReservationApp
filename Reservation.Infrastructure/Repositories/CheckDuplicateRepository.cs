using System;
using Reservation.Domain.Repositories;
using Reservation.Infrastructure.DbContexts;

namespace Reservation.Infrastructure.Repositories
{
    public class CheckDuplicateRepository : ICheckDuplicateRepository
    {
        private readonly ReservationDbContext _context;
        public CheckDuplicateRepository(ReservationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// 同日に予約が同じ人で行われてるか
        /// </summary>
        /// <param name="date"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool CheckDuplicate(DateOnly date, string clientId)
        {
            return false;
        }
    }
}

