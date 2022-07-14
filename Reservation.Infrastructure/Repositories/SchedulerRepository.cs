using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Models.ScheduleModels.Enums;
using Reservation.Domain.Models.ScheduleModels;
using Reservation.Domain.Repositories;
using Reservation.Infrastructure.DbContexts;
using Reservation.Infrastructure.Models;
using Reservation.Infrastructure.Models.ModelDTO;
using System;

namespace Reservation.Infrastructure.Repositories
{
    public class SchedulerRepository: ISchedulerRepository
    {
        private readonly ReservationDbContext _context;
        public SchedulerRepository(ReservationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// DBから指定日付範囲のスケジュールリスト取得
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public async Task<List<ScheduleModel>> GetSchedulesAsync(DateOnly fromDate, DateOnly toDate)
        {
            var schedules = await _context.Schedules
                            .Include(x => x.PanelStatus)
                            .Where(x => x.ScheduleDate >= fromDate.ToDateTime(new TimeOnly())
                                && x.ScheduleDate <= toDate.ToDateTime(new TimeOnly()))
                            .ToListAsync();

            List<ScheduleModel> result = new List<ScheduleModel>();

            schedules.ForEach(x =>
            {
                result.Add(ScheduleDTO.FromDBtoDomainScheduleAsync(x));
            });

            return result;
        }

        /// <summary>
        /// SheculeModel単体を取得します
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<ScheduleModel?> GetScheduleAsync(string scheduleId)
        {
            var schedule = await _context.Schedules
                        .Include(x => x.PanelStatus)
                        .Include(x => x.PanelStatus.Client)
                        .Where(x => x.ScheduleId == scheduleId)
                        .FirstOrDefaultAsync();

            if (schedule == null)
                return null;

            return ScheduleDTO.FromDBtoDomainScheduleAsync(schedule);
        }

        /// <summary>
        /// 仮予約
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task MakeTemporaryReservation(ScheduleModel schedule)
        {
            var db = await _context.Schedules
                            .Include(x => x.PanelStatus)
                            .Include(x => x.PanelStatus.Client)
                            .Where(x => x.ScheduleId == schedule.ScheduleId)
                            .FirstAsync();

            db.PanelStatus.Status = (int)schedule.PanelStatus;
            db.PanelStatus.Client = await _context.Clients.Where(x => x.ClientId == schedule.ClientId).FirstAsync();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CancelTemporaryReservation(ScheduleModel schedule)
        {
            var db = await _context.Schedules
                            .Include(x => x.PanelStatus)
                            .Where(x => x.ScheduleId == schedule.ScheduleId)
                            .FirstAsync();
            db.PanelStatus.Status = (int)PanelStatusType.Blank;
            db.PanelStatus.Client = null;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 予約承認
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ApproveReservaton(ScheduleModel schedule)
        {
            var db = await _context.Schedules
                            .Include(x => x.PanelStatus)
                            .Where(x => x.ScheduleId == schedule.ScheduleId)
                            .FirstAsync();
            db.PanelStatus.Status = (int)schedule.PanelStatus;
            db.PanelStatus.Manager = await _context.Managers.Where(x => x.ManagerId == schedule.ManagerId).FirstAsync();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 予約却下
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task NotApproveReservaton(ScheduleModel schedule)
        {
            var db = await _context.Schedules
                            .Include(x => x.PanelStatus)
                            .Where(x => x.ScheduleId == schedule.ScheduleId)
                            .FirstAsync();
            db.PanelStatus.Status = (int)schedule.PanelStatus;
            db.PanelStatus.Client = null;
            db.PanelStatus.Manager = null;
            await _context.SaveChangesAsync();
        }
    }
}

