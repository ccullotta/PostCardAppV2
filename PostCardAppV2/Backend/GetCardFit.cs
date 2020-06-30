using PostCardAppV2.Data;
using PostCardAppV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Backend
{
    public class GetCardFit
    {
        private PostCardAppContext _context;
        private Paper paper;
        private double length;
        private double width;
        private bool withbleed;
        private List<Sheets> sheets;
        private Sheets BestSheet;
        private int count;
        public GetCardFit(Paper paper, CardSize size, bool withbleed, PostCardAppContext context)
        {
            try
            {
                _context = context;
                this.paper = paper;
                length = withbleed ? size.length + 0.25 : size.length;
                width = withbleed ? size.width + 0.25 : size.width;
                this.withbleed = withbleed;
                this.sheets = paper.CostAssignments.Select(x => x.Sheet).OrderBy(x => x.size).ToList();
                getBestFit();
            } catch (ArgumentNullException e)
            {
                throw new InvalidOperationException("Required Includes were possibly missing for getcardfit!" + e.Message);
            }
            if(count == 0)
            {
                throw new InvalidOperationException("No Compatible Sheet Sizes Found");
            }

        }

        private void getBestFit()
        {
            int max = 0;
            Sheets best = null;

            for(int i = 0; i < sheets.Count; i++)
            {
                var check = getFit(sheets[i]);
                if(check > max)
                {
                    max = check;
                    best = sheets[i];
                } 
            }
            BestSheet = best;
            count = max;
            
        }

        private int getFit(Sheets sheets)
        {
            var sheetL = sheets.length;
            var sheetW = sheets.width;

            var lengthsDown = Math.Floor(sheetL / length);
            var widthwise = lengthsDown * Math.Floor(sheetW / width);
            var remainingLength = sheetL - (lengthsDown * length);
            var lengthwise = Math.Floor(remainingLength / width) * Math.Floor(sheetW / length);
            int totalMethodOne = (int)(lengthwise + widthwise);

            var lengthsAcross = (int)Math.Floor(sheetW / length);
            int lengthwise2 = lengthsAcross * (int)Math.Floor(sheetL / width);
            var remainingwidth2 = sheetW - (lengthsAcross * width);
            int widthwise2 = (int)(Math.Floor(remainingwidth2 / width) * Math.Floor(sheetL / length));
            int totalMethodTwo = lengthwise2 + widthwise2;

            return Math.Max(totalMethodOne, totalMethodTwo);

        }

        public Sheets GetBestSheet()
        {
            return BestSheet;
        }

        public int GetBestFitCount()
        {
            return count;
        }
    }
}
