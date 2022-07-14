using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Models
{
    public class Schedule : IDbModel
    {
        [Key]
        public string ScheduleId { get; set; }

        public DateTime ScheduleDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual PanelStatus PanelStatus { get; set; }
    }
}
