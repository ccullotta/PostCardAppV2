using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Models
{
    public class CardColor
    {
        public int ID { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [Required]
        public double Multiplier { get; set; }

    }
}
