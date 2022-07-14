using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Models
{
    public class Client: IDbModel
    {
        public Client()
        {
            PanelStatuses = new HashSet<PanelStatus>();
        }

        [Key]
        public string ClientId { get; set; }

        [Required]
        public string ClientLoginId { get; set; }

        [Required]
        public string ClientName { get; set; }

        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? HealthInsuranceNumber { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        public ClientStatus ClientStatus { get; set; }

        /// <summary>
        /// AspNetUsers.Id
        /// </summary>
        public string? OwnerID { get; set; }


        public virtual ICollection<PanelStatus> PanelStatuses { get; set; }
    }

    public enum ClientStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
