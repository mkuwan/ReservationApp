using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Models
{
    public class Manager : IDbModel
    {
        public Manager()
        {
            PanelStatuses = new HashSet<PanelStatus>();
        }

        [Key]
        public string ManagerId { get; set; }

        [Required]
        public string ManagerLoginId { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        public ManagerStatus ManagerStatus { get; set; }

        /// <summary>
        /// AspNetUsers.Id
        /// </summary>
        public string? OwnerID { get; set; }

        public virtual ICollection<PanelStatus> PanelStatuses { get; set; }
    }

    public enum ManagerStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
