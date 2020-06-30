using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Models
{
    public class Sheets
    {
        public int ID { get; set; }
        [MaxLength(10)]
        private string _name;

        [Required]
        [Display(Name = "Sheet Size")]
        [RegularExpression(@"^[0-9]+\.?[0-9]*[xX]{1}[0-9]+\.?[0-9]*$", ErrorMessage = "format must be of the form: ##x##")]
        [MaxLength(10)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                var nums = _name.Split("x");
                var num1 = double.Parse(nums[0]);
                var num2 = double.Parse(nums[1]);
                length = Math.Max(num1, num2);
                width = Math.Min(num1, num2);
            }
        }
        public double size { get { return length * width; } }
        
        public double length { get; private set; }
        public double width { get; private set; }

        public ICollection<PaperSheetAssignments> PaperAssignments { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Sheets sheets &&
                   _name == sheets._name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }
    }
}
