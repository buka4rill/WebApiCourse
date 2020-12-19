﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiPSCourse.Data;

namespace WebApiPSCourse.Migrations
{
    [DbContext(typeof(CampContext))]
    partial class CampContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiPSCourse.Data.Entities.Camp", b =>
                {
                    b.Property<int>("CampId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Moniker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CampId");

                    b.HasIndex("LocationId");

                    b.ToTable("Camps");

                    b.HasData(
                        new
                        {
                            CampId = 1,
                            EventDate = new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Length = 1,
                            LocationId = 1,
                            Moniker = "LAG2019",
                            Name = "Made in Lagos Camp"
                        });
                });

            modelBuilder.Entity("WebApiPSCourse.Data.Entities.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityTown")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateProvince")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VenueName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Location");

                    b.HasData(
                        new
                        {
                            LocationId = 1,
                            Address1 = "123 Ikorodu Road",
                            CityTown = "Ikorodu",
                            Country = "Nigeria",
                            PostalCode = "123123",
                            StateProvince = "LA",
                            VenueName = "Lagos Conservation Centre"
                        });
                });

            modelBuilder.Entity("WebApiPSCourse.Data.Entities.Speaker", b =>
                {
                    b.Property<int>("SpeakerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BlogUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpeakerId");

                    b.ToTable("Speakers");

                    b.HasData(
                        new
                        {
                            SpeakerId = 1,
                            BlogUrl = "https://github.com/buka4rill",
                            Company = "Buka Investments",
                            CompanyUrl = "http://bukainvestments.com",
                            FirstName = "Ebuka",
                            LastName = "Abraham",
                            Twitter = "buka4rill"
                        },
                        new
                        {
                            SpeakerId = 2,
                            BlogUrl = "https://adalovelace.com",
                            Company = "Women in Tech LLC",
                            CompanyUrl = "http://womenintech.in",
                            FirstName = "Ada",
                            LastName = "Lovelace",
                            Twitter = "adalovelace"
                        });
                });

            modelBuilder.Entity("WebApiPSCourse.Data.Entities.Talk", b =>
                {
                    b.Property<int>("TalkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abstract")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CampId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("SpeakerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TalkId");

                    b.HasIndex("CampId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("Talks");

                    b.HasData(
                        new
                        {
                            TalkId = 1,
                            Abstract = "Thinking of good sample data example is tiring.",
                            CampId = 1,
                            Level = 200,
                            SpeakerId = 1,
                            Title = "Sample Data Made Easy"
                        },
                        new
                        {
                            TalkId = 2,
                            Abstract = "Entity Framework from scratch in an hour.",
                            CampId = 1,
                            Level = 100,
                            SpeakerId = 2,
                            Title = "Entity Framework from Scratch"
                        });
                });

            modelBuilder.Entity("WebApiPSCourse.Data.Entities.Camp", b =>
                {
                    b.HasOne("WebApiPSCourse.Data.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("WebApiPSCourse.Data.Entities.Talk", b =>
                {
                    b.HasOne("WebApiPSCourse.Data.Entities.Camp", "Camp")
                        .WithMany("Talks")
                        .HasForeignKey("CampId");

                    b.HasOne("WebApiPSCourse.Data.Entities.Speaker", "Speaker")
                        .WithMany()
                        .HasForeignKey("SpeakerId");
                });
#pragma warning restore 612, 618
        }
    }
}