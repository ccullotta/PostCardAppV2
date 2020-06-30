using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Models
{
    public class Quote
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name ="Customer")]
        public string CustomerName { get; set; }

        [Display(Name = "Bleed:")]
        public bool WithBleed { get; set; }

        [Display(Name = "Paper")]
        [MaxLength(60)]
        public string Paper { get; set; }

        [Display(Name = "CardSize")]
        [MaxLength(20)]
        public string CardSize { get; set; }

        [Display(Name = "Color")]
        [MaxLength(30)]
        public string Color { get; set; }

        [Display(Name = "Created: ")]
        public DateTime CreatedOn { get; set; }

        public List<Estimate> Estimates { get; set; }

        public string DisplayBleed()
        {
            return WithBleed ? "With bleed" : "With-Out bleed";
        }

    }

}
