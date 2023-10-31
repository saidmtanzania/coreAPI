﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using coreAPI.Data;

#nullable disable

namespace coreAPI.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("coreAPI.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("beb25260-3c11-4bde-b638-d91b415710e6"),
                            Name = "EASY"
                        },
                        new
                        {
                            Id = new Guid("ba8baf41-f687-42a1-adc0-883461b41cb7"),
                            Name = "MEDIUM"
                        },
                        new
                        {
                            Id = new Guid("27ec554d-ef7c-49e2-ae6a-7f95205d7e40"),
                            Name = "HARD"
                        });
                });

            modelBuilder.Entity("coreAPI.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5636a3b1-e899-46e5-9fb3-a60a9e593fb7"),
                            Code = "TZ",
                            Name = "TANZANIA",
                            RegionImageUrl = "https://www.state.gov/wp-content/uploads/2018/11/Tanzania-e1555938157355-2501x1406.jpg"
                        },
                        new
                        {
                            Id = new Guid("97a6bd50-aa74-4dc5-b6de-1869db81d98f"),
                            Code = "KE",
                            Name = "KENYA",
                            RegionImageUrl = "https://destinationuganda.com/wp-content/uploads/2020/10/exploring-kampala-city-uganda-capital.jpg"
                        },
                        new
                        {
                            Id = new Guid("eb6fa6ba-f718-48f7-90a2-736da9197a70"),
                            Code = "UG",
                            Name = "UGANDA",
                            RegionImageUrl = "https://a.travel-assets.com/findyours-php/viewfinder/images/res70/38000/38950-Nairobi.jpg"
                        },
                        new
                        {
                            Id = new Guid("64a4ef3b-0b1f-406c-81ff-25af1f1ea478"),
                            Code = "DZ",
                            Name = "ALGERIA",
                            RegionImageUrl = "https://lp-cms-production.imgix.net/2023-10/iStock-985914532-RFC.jpg"
                        },
                        new
                        {
                            Id = new Guid("ed82e0ce-5945-41b3-9cbe-1ed90d23cb26"),
                            Code = "AO",
                            Name = "ANGOLA",
                            RegionImageUrl = "https://unhabitat.org/sites/default/files/styles/featured_image_header_sm_focal/public/2019/05/shutterstock_1116891344.jpg"
                        });
                });

            modelBuilder.Entity("coreAPI.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("coreAPI.Models.Domain.Walk", b =>
                {
                    b.HasOne("coreAPI.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("coreAPI.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
