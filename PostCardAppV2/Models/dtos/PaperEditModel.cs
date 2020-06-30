using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static PostCardAppV2.Models.Paper;

namespace PostCardAppV2.Models.dtos
{
    public class PaperEditModel 
    {
        public int ID { get; set; }

        public string Name
        {
            get
            {
                var ret = Weight + " " + PaperCoating + (PaperColor.Equals("white") ? "" : (" " + PaperColor));
                return ret;
            }
        }
        [Required]
        [MaxLength(10)]
        public string Weight { get; set; }
        [Required]
        public StockType PaperStockType { get; set; }

        [Required]
        public Coating PaperCoating { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaperColor { get; set; }

        public List<PaperEditAssignments> EditAssignments { get; set; }
    }

    public class PaperEditAssignments
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
