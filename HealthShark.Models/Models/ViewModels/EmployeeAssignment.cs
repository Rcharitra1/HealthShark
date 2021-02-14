using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.Models.Models.ViewModels
{
    public class EmployeeAssignment
    {
        public UserVM UserVM { get; set; }

        public UserAssignmentVM UserAssignmentVM { get; set; }

        public List<BodyType> bodyTypes { get; set; }

        public List<WorkOutType> workOutTypes { get; set; }
        
        public List<Diet> diets { get; set; } 
    }
}
