using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostCardAppV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new PostCardAppContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<PostCardAppContext>>()))
            {
                // Look for any papers.
                //if (context.Papers.Any())
                //{
                //    return;   // DB has been seeded
                //}

                //context.Sheets.AddRange(
                //    new Sheets
                //    {
                //        Name = "5x3",
                //    }
                //    );
                //context.SaveChanges();

                //context.Papers.AddRange(
                //    new Paper
                //    {

                //        Name = "80# glossy hehe",
                //    }
                //);
                //context.SaveChanges();

                //context.PaperSheetAssignments.AddRange(
                //        new List<PaperSheetAssignments>(){
                //                new PaperSheetAssignments
                //            {
                //                PaperId = context.Papers.First(x=>x.Name == "80# glossy hehe").ID,
                //                SheetId = context.Sheets.First(x=>x.Name == "5x3").ID,
                //                Cost = 20,
                //            }
                //        }
                //    );
                //context.SaveChanges();
    //            context.SheetCardSizeAssignments.AddRange(
    //                    new List<SheetCardSizeAssignments>(){
    //                            new SheetCardSizeAssignments
    //                        {
    //                            SheetId = context.Sheets.First(x=>x.Name == "5x3").ID,
    //                        }
    //    }
    //);
    //            context.SaveChanges();
            }





        }
    }
}
