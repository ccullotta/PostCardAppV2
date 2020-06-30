using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Models.dtos
{
    [BindProperties]
    public class QuotesCreateDto
    {
        [Required]
        [MaxLength(40)]
        public string CustomerName { get; set; }
        public bool WithBleed { get; set; }
        public int PaperId { get; set; }

        [Display(Name = "Paper")]
        [MaxLength(60)]
        public string Paper { get; set; }
        public int CardSizeId { get; set; }

        [Display(Name = "CardSize")]
        [MaxLength(20)]
        public string CardSize { get; set; }

        public int ColorId { get; set; }
        [Display(Name = "Color")]
        [MaxLength(30)]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Quantity: ")]
        public int Quantity { get; set; }

        [Display(Name = "Price: ")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public double? Price { get; set; }
        public List<Estimate> QuantitiesAndPrices { get; set; }

    }


}
