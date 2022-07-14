using Reservation.Infrastructure.DbContexts;
using Reservation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reservation.Domain.Models.ScheduleModels.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Reservation.Infrastructure.RoleOperations;

namespace Reservation.Infrastructure.DummyData
{
    public static class SeedData
    {
        /// <summary>
        /// 既存データの削除
        /// </summary>
        /// <param name="reservationDbContext"></param>
        public static async Task DeleteDate(ReservationDbContext reservationDbContext)
        {

            // Delete Data
            if (reservationDbContext.Schedules.Any())
            {
                reservationDbContext.Schedules.RemoveRange(reservationDbContext.Schedules);
                await reservationDbContext.SaveChangesAsync();
            }

            if (reservationDbContext.PanelStatuses.Any())
            {
                reservationDbContext.PanelStatuses.RemoveRange(reservationDbContext.PanelStatuses);
                await reservationDbContext.SaveChangesAsync();
            }

            if (reservationDbContext.Clients.Any())
            {
                reservationDbContext.Clients.RemoveRange(reservationDbContext.Clients);
                await reservationDbContext.SaveChangesAsync();
            }

            if (reservationDbContext.Managers.Any())
            {
                reservationDbContext.Managers.RemoveRange(reservationDbContext.Managers);
                await reservationDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// jsonから初期データを作成します
        /// </summary>
        /// <param name="reservationDbContext"></param>
        public static async Task InitializeAspNetUsersClient(IServiceProvider serviceProvider, string initialPassword)
        {

            using (var context = new ReservationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ReservationDbContext>>()))
            {
                if (context.Clients.Any()) return;

                // read data from Json
                var clients = GetModelData<Client>("DummyClient.json");

                if (clients == null) return;

                foreach (var client in clients)
                {
                    var clientId = await EnsureUser(serviceProvider, client.ClientLoginId, initialPassword);
                    await EnsureRole(serviceProvider, clientId, RoleOperationConstants.ReservationClientRole);

                    client.ClientStatus = ClientStatus.Approved;
                    client.OwnerID = clientId;
                    context.Clients.Add(client);
                }

                await context.SaveChangesAsync();
            }

        }

        public static async Task InitializeAspNetUsersManaber(IServiceProvider serviceProvider, string initialPassword)
        {
                // UserSecret を使う方法: ローカルPC内の指定のディレクトリ内で管理します
                // これはあくまでも開発環境用としてください
                // Password is set with the following:
                // dotnet user-secrets init --project Reservation.API
                // dotnet user-secrets set --project Reservation.API SeedUserPW <pw>

            using (var context = new ReservationDbContext(
                            serviceProvider.GetRequiredService<DbContextOptions<ReservationDbContext>>()))
            {
                if (context.Managers.Any()) return;

                // read data from Json
                var managers = GetModelData<Manager>("DummyManager.json");

                if (managers == null) return;

                foreach (var manager in managers)
                {
                    var managerId = await EnsureUser(serviceProvider, manager.ManagerLoginId, initialPassword);
                    await EnsureRole(serviceProvider, managerId, RoleOperationConstants.ReservationManagersRole);

                    manager.ManagerStatus = ManagerStatus.Approved;
                    manager.OwnerID = managerId;
                    context.Managers.Add(manager);
                }
                await context.SaveChangesAsync();

            }
        }

        /// <summary>
        /// Create IdentityUser
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="manager"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string loginUserName, string testPassword)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(loginUserName);

            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = loginUserName,
                    EmailConfirmed = true
                };

                var identityResult = await userManager.CreateAsync(user, testPassword);
            }

            if (user == null)
                throw new Exception("パスワードの強度が不十分です");

            return user.Id;
        }

        /// <summary>
        /// User Role
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string userId, string role)
        {
            var roleManaber = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManaber == null)
                throw new Exception("RoleManagerがnullです");

            IdentityResult identityResult;
            if (!await roleManaber.RoleExistsAsync(role))
                identityResult = await roleManaber.CreateAsync(new IdentityRole(role));

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("パスワードの強度が不十分です");

            identityResult = await userManager.AddToRoleAsync(user, role);


            return identityResult;
        }

        /// <summary>
        /// コードで初期データを作成します
        /// jsonで作るのが面倒だったので....www
        /// </summary>
        /// <param name="reservationDbContext"></param>
        public static async Task InitializeSchedule(ReservationDbContext reservationDbContext)
        {

            var today = DateOnly.FromDateTime(DateTime.Today);
            
            var startMonday = today.AddDays(((int)today.DayOfWeek * -1) + 1);

            // 当該週の月曜日から4週間後までデータを作成します
            for(int day = 0; day < 28; day++)
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
                        ScheduleId = Guid.NewGuid().ToString(),
                        ScheduleDate = thisDay.ToDateTime(new TimeOnly()),
                        StartTime = thisDay.ToDateTime(start),
                        EndTime = thisDay.ToDateTime(end)
                    };

                    reservationDbContext.Schedules.Add(schedule);

                    var panelStatus = new PanelStatus()
                    {
                        PanelStatusId = schedule.ScheduleId,
                        Status = (int)PanelStatusType.Blank,
                        Client = null,
                        Manager = null
                    };

                    reservationDbContext.PanelStatuses.Add(panelStatus);
                }
                await reservationDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// jsonからModelへの変換
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="jsonFileName"></param>
        /// <returns>IEnumerable<Model></returns>
        private static IEnumerable<T>? GetModelData<T>(string jsonFileName) where T: IDbModel
        {
            string jsonPath = @$"{Directory.GetParent(Directory.GetCurrentDirectory())}\Reservation.Infrastructure\DummyData\{jsonFileName}";

            IEnumerable<T>? models = null;

            using (var file = File.OpenRead(jsonPath))
            {
                using(var reader = new StreamReader(file))
                {
                    var stringJson = reader.ReadToEnd();
                    models = JsonConvert.DeserializeObject<IEnumerable<T>>(stringJson);
                }
            }

            return models;
        }
    }
}
