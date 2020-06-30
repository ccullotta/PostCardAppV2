using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Models
{
    public class Estimate
    {
        public Estimate()
        {
        }
        public Estimate(int quantity, double price)
        {
            Quantity = quantity;
            Price = price;
        }

        public int ID { get; set; }
        [Required]

        public int QuoteId { get; set; }
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
