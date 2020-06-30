using PostCardAppV2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCardAppV2.Models 
{
    public class Paper : IEquatable<Paper>
    {
        public int ID { get; set; }


        [MaxLength(10)]
        public string Weight { get; set; }

        [Required]
        public StockType? PaperStockType { get; set; }
        [Required]
        public Coating PaperCoating { get; set; }

        [MaxLength(20)]
        [Required]
        public string PaperColor { get; set; }

        public string Name
        {
            get
            {
                var ret = Weight + " " + PaperCoating + (PaperColor.Equals("white") ? "" : (" " + PaperColor));
                return ret;
            }
        }

        [Display(Name = "Costs")]
        public ICollection<PaperSheetAssignments> CostAssignments { get; set; }

        [Display(Name = "Compatible Sizes")]
        public string CompatibleSizes { get; set; }


        public void UpdateSizes(PostCardAppContext context)
        {
            try
            {
                var compatibleSizes = GetCompatibleSizes(context);
                StringBuilder ret = new StringBuilder();
                foreach (var x in compatibleSizes)
                {
                    ret.Append(x.Name + ", ");
                }
                CompatibleSizes = ret.ToString();
            } catch (ArgumentNullException e)
            {
                throw new InvalidOperationException("updateSizes requires costassignments and sheets to be included in principle caller" + e.Message);
            }


        }

        public List<CardSize> GetCompatibleSizes(PostCardAppContext context)
        {
            try
            {
                var sheets = CostAssignments.Select(x => x.Sheet).OrderByDescending(x => x.size);
                var checkSheetList = sheets.TakeWhile(x => x.length >= sheets.First().length || x.width >= sheets.First().width).ToList();
                List<CardSize> compatibleSizes = new List<CardSize>();
                var allcardsizes = context.CardSize.ToHashSet();
                foreach (var s in checkSheetList)
                {
                    var sizes = allcardsizes.Where(x => x.length <= s.length && x.width <= s.width).ToList();
                    compatibleSizes.AddRange(sizes);
                    allcardsizes.ExceptWith(sizes);
                }
                return compatibleSizes.OrderBy(x => x.size).ToList();
            } catch (ArgumentNullException e)
            {
                throw new InvalidOperationException("GetCompatibleSizes requires costassignments and sheets to be included in principle caller" + e.Message);
            }

        }
        public override string ToString()
        {
            return Name; 
        }

        public override bool Equals(object obj)
        {
            return obj is Paper paper &&
                   Weight == paper.Weight &&
                   PaperStockType == paper.PaperStockType &&
                   PaperCoating == paper.PaperCoating &&
                   PaperColor == paper.PaperColor;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Weight, PaperStockType, PaperCoating, PaperColor);
        }

        public bool Equals([AllowNull] Paper other)
        {
            return base.Equals(other);
        }
    }
    public enum StockType
    {
        cover = 1,
    }
    public enum Coating
    {
        uncoated = 1,
        coated = 2,
        C1S = 3,
        C2S = 4,

    }
}
