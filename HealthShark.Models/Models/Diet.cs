using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthShark.Models.Models
{
    public class Diet
    {
        [Key]

        public int Id { get; set; }

        [Required]

        [MaxLength(100)]

        public string Type { get; set; }


        [MaxLength(200)]

        public string Description { get; set; }




    }
}
