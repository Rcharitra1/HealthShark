using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthShark.Models.Models.ViewModels
{
    public class UserAssignmentVM
    {
        [Key]

        public int Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]


        public ApplicationUser User { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]

        public string TrainerId { get; set; }

        [ForeignKey("TrainerId")]

        public ApplicationUser Trainer { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]

        public string DieticianId { get; set; }

        [ForeignKey("DieticianId")]

        public ApplicationUser Dietician { get; set; }


        public int? DietId { get; set; }

        [ForeignKey("DietId")]

        public Diet Diet { get; set; }

        public int? WorkoutId { get; set; }

        [ForeignKey("WorkoutId")]
        public WorkOutType WorkOut { get; set; }

        public int? BodyTypeId { get; set; }
        [ForeignKey("BodyTypeId")]

        public BodyType BodyType { get; set; }

        public int? UserPlanId { get; set; }

        [ForeignKey("UserPlanId")]


        public UserPlan UserPlan { get; set; }


        public bool? Paid { get; set; }








    }
}
