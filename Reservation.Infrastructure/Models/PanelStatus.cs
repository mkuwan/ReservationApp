using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Models
{
    public class PanelStatus : IDbModel
    {
        /// <summary>
        /// ステータス: Scheduleテーブルとは1-1 : ScheduleId = PanelStatusId
        ///            Clientとはn-1
        ///            Managerとはn-1
        /// </summary>
        [Key]
        [ForeignKey(nameof(Schedule))]
        public string PanelStatusId { get; set; }

        public int Status { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual Client? Client { get; set; }
        public virtual Manager? Manager { get; set; }
    }
}
