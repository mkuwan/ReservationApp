using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.RoleOperations
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create = 
            new OperationAuthorizationRequirement { Name = RoleOperationConstants.CreateOperationName };
        public static OperationAuthorizationRequirement Read = 
            new OperationAuthorizationRequirement { Name = RoleOperationConstants.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = RoleOperationConstants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = RoleOperationConstants.DeleteOperationName };
        public static OperationAuthorizationRequirement Approve =
          new OperationAuthorizationRequirement { Name = RoleOperationConstants.ApproveOperationName };
        public static OperationAuthorizationRequirement Reject =
          new OperationAuthorizationRequirement { Name = RoleOperationConstants.RejectOperationName };

    }
}
