using System.ComponentModel.DataAnnotations;

namespace PostCardAppV2.Models
{
    public class PaperSheetAssignments
    {
        public int ID { get; set; }
        public int PaperId { get; set; }
        public int SheetId { get; set; }

        public Paper Paper { get; set; }
        public Sheets Sheet { get; set; }

        public double Cost { get; set; }

        public override string ToString()
        {
            return Sheet.Name + ": $" + Cost;
        }
    }
}