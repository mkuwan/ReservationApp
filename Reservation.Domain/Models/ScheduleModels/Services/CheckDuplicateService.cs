using Microsoft.Extensions.DependencyInjection;
using Reservation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Models.ScheduleModels.Services
{
    public class CheckDuplicateService : ICheckDuplicateService
    {
        private readonly ICheckDuplicateRepository _checkDuplicateRepository;

        public CheckDuplicateService(ICheckDuplicateRepository checkDuplicateRepository)
        {
            _checkDuplicateRepository = checkDuplicateRepository;
        }

        public bool ChedkDuplicate(DateOnly date, string scheduleId)
        {
            return _checkDuplicateRepository.CheckDuplicate(date, scheduleId);
        }
    }
}


