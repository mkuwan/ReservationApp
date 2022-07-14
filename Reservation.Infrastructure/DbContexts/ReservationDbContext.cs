using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.DbContexts
{
    public class ReservationDbContext : IdentityDbContext
    {
        public ReservationDbContext()
        {

        }

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// 最初のマイグレーションを作成する (この時、既存DBがあるため、変更なしとするために -IgnoreChanges を指定する)
            //https://docs.microsoft.com/ja-jp/ef/core/get-started/wpf
            //https://docs.microsoft.com/ja-jp/ef/core/managing-schemas/migrations/?tabs=vs
            // EntityFrameworkCore\Add-Migration -Context ReservationDbContext InitialCreate(適当)
            // EntityFrameworkCore\Update-Database -Verbose -Context ReservationDbContext
            // これを使用する際は以下を利用し、それ以外はコメントにしてください
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=reservation.db");
                return;
            }
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<PanelStatus> PanelStatuses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
