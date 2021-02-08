using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthShark.Models.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }



  


        public string Role { get; set; }

        [NotMapped]
        public string Address { get 
            {
                return StreetAddress + ", " + City + ", " + Province + ", " + PostalCode;

            }  
        }
    }
}
