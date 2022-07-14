using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.RoleOperations
{
    public class RoleOperationConstants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string RejectOperationName = "Reject";

        public static readonly string ReservationAdministratorsRole = "ReservationAdministrators";
        public static readonly string ReservationManagersRole = "ReservationManagers";

        public static readonly string ReservationClientRole = "ReservationClient";
    }
}
