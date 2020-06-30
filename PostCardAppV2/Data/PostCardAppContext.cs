using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostCardAppV2.Models;

namespace PostCardAppV2.Data
{
    public class PostCardAppContext : DbContext
    {
        public PostCardAppContext(DbContextOptions<PostCardAppContext> options) : base(options)
        {

        }

        public DbSet<Paper> Papers { get; set; }

        public DbSet<Sheets> Sheets { get; set; }

        public DbSet<CardColor> CardColor { get; set; }

        public DbSet<CardSize> CardSize { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<PaperSheetAssignments> PaperSheetAssignments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaperSheetAssignments>()
                .HasOne(p => p.Paper)
                .WithMany(a => a.CostAssignments);

            modelBuilder.Entity<PaperSheetAssignments>()
                .HasOne(s => s.Sheet)
                .WithMany(a => a.PaperAssignments);


        }
    }
}
