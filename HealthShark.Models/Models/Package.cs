using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthShark.Models.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }

        [Required]

        [StringLength(100)]

        public string Name { get; set; }

        [Required]

        [StringLength(100)]

        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public double CouplePrice { get; set; }


    }
}
