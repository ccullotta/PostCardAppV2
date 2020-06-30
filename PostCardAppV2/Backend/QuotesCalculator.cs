using PostCardAppV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Backend
{

    public class QuotesCalculator
    {
        private double TotalCost;
        private int totalSheets;
        private double paperMult;
        private double CostPerSheet;
        double paperCost;
        double clickCharge;
        double clickMult;
        private static readonly int setup = 25;
        private int quantity;
        static double[] costRange = { 0, 100, 250, 500, 1000, 1500, 2500, 4000, 7000 };
        static double[] paperTable = { 1.45, 1.42, 1.39, 1.36, 1.33, 1.31, 1.29, 1.27, 1.25 };
        private double clickTotal;
        private double[] clickTable = { 8, 6, 5, 4, 3.5, 3.2, 2.5, 2, 1.75 };

        public QuotesCalculator(int quantity, int upOnSheet, double costPerSheet, CardColor type)
        {
            CostPerSheet = costPerSheet;
            clickCharge = type.Multiplier;
            totalSheets = quantity / upOnSheet;
            this.quantity = quantity;
            calcPrintCost();
        }

        private void calcPrintCost()
        {
            calcPaperCost();
            calcClickTotal();
            TotalCost = paperCost + clickTotal + setup;
        }

        private void calcClickTotal()
        {
            for (int i = costRange.Count() - 1; i >= 0; i--)
            {
                if (quantity > costRange[i])
                {
                    clickMult = clickTable[i];
                    break;
                }
            }
            clickTotal = clickMult * clickCharge * totalSheets;
        }

        private void calcPaperCost()
        {
            for (int i = costRange.Count() - 1; i >= 0; i--)
            {
                if (quantity > costRange[i])
                {
                    paperMult = paperTable[i];
                    break;
                }
            }
            paperCost = paperMult * CostPerSheet * totalSheets;
        }

        public double getTotalCost()
        {
            return TotalCost;
        }

    }

}
