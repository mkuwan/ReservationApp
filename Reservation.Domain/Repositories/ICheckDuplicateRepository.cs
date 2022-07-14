using System;
namespace Reservation.Domain.Repositories
{
    public interface ICheckDuplicateRepository
    {
        bool CheckDuplicate(DateOnly date, string clientId);
    }
}

