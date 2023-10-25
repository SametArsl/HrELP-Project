using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Domain.Entities.Enums
{
    public enum ApprovalStatus
    {
        Approved,
        [Display(Name = "Pending Approval")]
        Pending_Approval,
        Declined
    }
}
