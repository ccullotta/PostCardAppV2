using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static PostCardAppV2.Models.Paper;

namespace PostCardAppV2.Models.dtos
{
    public class PaperCreateModel
    {
        //[Required]
        //[MaxLength(40)]
        //public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "Weight")]
        public string Weight { get; set; }
        [Required]
        [Display(Name = "Stock Type")]
        public StockType? PaperStockType { get; set; }

        [Required]
        [Display(Name = "Coating")]
        public Coating? PaperCoating { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Color")]
        public string PaperColor { get; set; }

        [DictionaryValidation]
        public Dictionary<string, string> SheetPricing { get; set; }
    }
}
