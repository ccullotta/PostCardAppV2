using PostCardAppV2.Models;
using System.Collections.Generic;

namespace PostCardAppV2.Controllers
{
    public class PaperViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        List<PaperSheetAssignments> AssignedCosts { get; set; }
        public List<CardSize> CompatibleSizes { get; set; }
    }
}