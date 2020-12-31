using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthShark.Models.Models
{
    public class WorkOutType
    {
        [Key]

        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Type { get; set; }


        [Required]
        [MaxLength(200)]

        public string Description { get; set; }

    }
}
