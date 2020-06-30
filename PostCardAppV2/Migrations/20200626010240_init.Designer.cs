﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostCardAppV2.Data;

namespace PostCardAppV2.Migrations
{
    [DbContext(typeof(PostCardAppContext))]
    [Migration("20200626010240_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PostCardAppV2.Models.CardColor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Multiplier")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("CardColor");
                });

            modelBuilder.Entity("PostCardAppV2.Models.CardSize", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<double>("width")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("CardSize");
                });

            modelBuilder.Entity("PostCardAppV2.Models.Paper", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompatibleSizes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.ToTable("Papers");
                });

            modelBuilder.Entity("PostCardAppV2.Models.PaperSheetAssignments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("PaperId")
                        .HasColumnType("int");

                    b.Property<int>("SheetId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PaperId");

                    b.HasIndex("SheetId");

                    b.ToTable("PaperSheetAssignments");
                });

            modelBuilder.Entity("PostCardAppV2.Models.quoteInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardSizeId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("PaperId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("WithBleed")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("CardSizeId");

                    b.HasIndex("ColorId");

                    b.HasIndex("PaperId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("PostCardAppV2.Models.Sheets", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<double>("length")
                        .HasColumnType("float");

                    b.Property<double>("width")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("PostCardAppV2.Models.PaperSheetAssignments", b =>
                {
                    b.HasOne("PostCardAppV2.Models.Paper", "Paper")
                        .WithMany("CostAssignments")
                        .HasForeignKey("PaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PostCardAppV2.Models.Sheets", "Sheet")
                        .WithMany("PaperAssignments")
                        .HasForeignKey("SheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostCardAppV2.Models.quoteInfo", b =>
                {
                    b.HasOne("PostCardAppV2.Models.CardSize", "CardSize")
                        .WithMany()
                        .HasForeignKey("CardSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PostCardAppV2.Models.CardColor", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PostCardAppV2.Models.Paper", "Paper")
                        .WithMany()
                        .HasForeignKey("PaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
