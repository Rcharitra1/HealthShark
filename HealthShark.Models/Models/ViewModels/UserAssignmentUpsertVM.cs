using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.Models.Models.ViewModels
{
    public class UserAssignmentUpsertVM
    {
        public int PlanId { get; set; }

        public UserPlan Plan { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
