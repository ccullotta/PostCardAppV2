using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Models
{
    public class CardSize
    {
        [MaxLength(10)]
        private string _name;

        public int ID { get; set; }

        [Required]
        [Display(Name = "Card Name")]
        [RegularExpression(@"^[0-9]+\.?[0-9]*[xX]{1}[0-9]+\.?[0-9]*$", ErrorMessage = "format must be of the form: ##x##")]
        [MaxLength(10)]
        public string Name { 
            get {
                return _name;
            } set {
                _name = value;
                var nums = _name.Split("x");
                var num1 = double.Parse(nums[0]);
                var num2 = double.Parse(nums[1]);
                length = Math.Max(num1, num2);
                width = Math.Min(num1, num2);
            } }

        public double length { get; set; }
        public double width { get; set; }
        [Display(Name = "Area")]
        public double size { get { return length * width; } }

    }
}
