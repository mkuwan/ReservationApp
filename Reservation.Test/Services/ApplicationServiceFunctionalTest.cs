using Xunit;
using Reservation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using Reservation.Infrastructure.Models;
using Reservation.Infrastructure.DbContexts;
using Reservation.Domain.Models.ScheduleModels.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace Reservation.Application.Services.Tests
{
    public class ApplicationServiceFunctionalTest : IDisposable
    {
        private DbConnection dbConnection { get; set; }  

        public ApplicationServiceFunctionalTest()
        {
            SeedData();
        }



        [Fact()]
        public void GetPanelAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void GetSchedulesAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void TakeReservationAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void CancelReservationAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ApproveReservationAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void NotApproveReservationAsyncTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// InMemorySqliteを使ってテストします
        /// </summary>
        private void SeedData()
        {
            dbConnection = new SqliteConnection("mode=memory");
            dbConnection.Open();
            var builder = new DbContextOptionsBuilder<ReservationDbContext>();
            // InMemoryではトランザクション(スコープ)を使うとエラーになってしまうので回避コード
            builder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning));
            var options = builder.UseSqlite(dbConnection).Options;

            using (var context = new ReservationDbContext(options))
            {
                var startMonday = DateTime.Today.AddDays(((int)DateTime.Today.DayOfWeek * -1) + 1);

                // 当該週の月曜日から2週間(14日間)のデータを作成します
                for (int day = 0; day < 14; day++)
                {
                    var thisDay = startMonday.AddDays(day);

                    // 土日は除く
                    if (thisDay.DayOfWeek == DayOfWeek.Sunday || thisDay.DayOfWeek == DayOfWeek.Saturday)
                        continue;

                    // 10時～17時
                    TimeOnly startTime = new TimeOnly(10, 0);
                    for (int i = 0; i < 14; i++)
                    {
                        // 12:00, 12:30, 13:00, 13:30 は除く
                        if (i >= 4 && i <= 7)
                            continue;

                        var start = startTime.AddMinutes(30 * i);
                        var end = startTime.AddMinutes(30 * i + 30);

                        var schedule = new Schedule()
                        {
                            ScheduleId = "APPLICATION_TEST" + Guid.NewGuid().ToString(),
                            ScheduleDate = thisDay,
                            StartTime = new DateTime(thisDay.Ticks + start.Ticks),
                            EndTime = new DateTime(thisDay.Ticks + end.Ticks)
                        };

                        context.Schedules.Add(schedule);
                        context.SaveChanges();

                        var panelStatus = new PanelStatus()
                        {
                            PanelStatusId = schedule.ScheduleId,
                            Status = (int)PanelStatusType.Blank,
                            Client = null,
                            Manager = null
                        };

                        context.PanelStatuses.Add(panelStatus);
                        context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// xUnitではDisposeでテストごとにデータのクリアが行われます
        /// </summary>
        public void Dispose()
        {
            dbConnection.Close();
        }
    }
}