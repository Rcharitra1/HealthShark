using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthShark.Models.Models.ViewModels
{
    public class UserVM
    {
        [Key]
        public int Id { get; set; }


        [Range(10.00, 500.00, ErrorMessage ="The weight can be a in 10 to 500 kgs range" )]

        public double Weight { get; set; }


        [Range(10.00, 300.00, ErrorMessage = "The weight can be a in 10 to 300 centimeters range")]

        public double Height { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string MedicalHistory { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull =true)]

        public string Allergies { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string ImageUrl { get; set; }


        public string UserId { get; set; }

        [ForeignKey("UserId")]

        public ApplicationUser User { get; set; }

        [NotMapped]

        public double BMI { get 
            
            {
                return (Weight/(Height*Height))/10000;
            }
        }
    }
}
