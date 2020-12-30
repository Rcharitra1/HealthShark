using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthShark.Models.Models
{
    public class BodyType
    {
        [Key]

        public int Id { get; set; }

        [Required]

        public string Description { get; set; }



    }
}
