﻿// <auto-generated />
using System;
using EventsDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventsDAL.Migrations
{
    [DbContext(typeof(EventContext))]
    [Migration("20240726094329_userRole_nullable")]
    partial class userRole_nullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventsDAL.Models.ErrorLog", b =>
                {
                    b.Property<Guid>("logId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Errormessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("logId");

                    b.ToTable("ErrorLogs");
                });

            modelBuilder.Entity("EventsDAL.Models.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventId");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EventDate");

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EventDescription");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EventName");

                    b.HasKey("EventId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = new Guid("f3883d6d-510c-4ce6-81de-48e24352096d"),
                            EventDate = new DateTime(2024, 7, 26, 15, 13, 28, 730, DateTimeKind.Local).AddTicks(7835),
                            EventDescription = "This is an annual event held by microsoft",
                            EventName = "Ignite"
                        },
                        new
                        {
                            EventId = new Guid("0d8f6a75-f3dd-42f0-ae0a-f91746b5b5db"),
                            EventDate = new DateTime(2024, 7, 26, 15, 13, 28, 730, DateTimeKind.Local).AddTicks(7854),
                            EventDescription = "This is an annual event held by microsoft",
                            EventName = "Microsoft Build"
                        });
                });

            modelBuilder.Entity("EventsDAL.Models.EventAccess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("EventAccesses");
                });

            modelBuilder.Entity("EventsDAL.Models.EventAllocation", b =>
                {
                    b.Property<Guid>("EventAllocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventAllocationId");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventId");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LocationId");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StaffId");

                    b.HasKey("EventAllocationId");

                    b.HasIndex("EventId");

                    b.HasIndex("LocationId");

                    b.HasIndex("StaffId");

                    b.ToTable("EventAllocations");
                });

            modelBuilder.Entity("EventsDAL.Models.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LocationId");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LocationName");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationId = new Guid("2b8fde25-82da-44c3-9a21-9eb25ba91519"),
                            LocationName = "Chennai"
                        },
                        new
                        {
                            LocationId = new Guid("f3e7f117-0dc3-454e-ba19-a3cbfc4a1160"),
                            LocationName = "Mumbai"
                        },
                        new
                        {
                            LocationId = new Guid("48a26bee-3d95-4521-b7a0-d7f7e51eeeb7"),
                            LocationName = "Seatle"
                        },
                        new
                        {
                            LocationId = new Guid("1ef18a59-7661-4955-a8fc-00cc10fcfcb2"),
                            LocationName = "LosAngels"
                        });
                });

            modelBuilder.Entity("EventsDAL.Models.Staff", b =>
                {
                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StaffId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LocationId");

                    b.Property<string>("StaffName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StaffName");

                    b.HasKey("StaffId");

                    b.HasIndex("LocationId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("EventsDAL.Models.TopicCovered", b =>
                {
                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TopicId");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventId");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LocationId");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StaffId");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TopicName");

                    b.HasKey("TopicId");

                    b.HasIndex("EventId");

                    b.HasIndex("LocationId");

                    b.HasIndex("StaffId");

                    b.ToTable("TopicsCovered");
                });

            modelBuilder.Entity("EventsDAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventsDAL.Models.EventAccess", b =>
                {
                    b.HasOne("EventsDAL.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsDAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventsDAL.Models.EventAllocation", b =>
                {
                    b.HasOne("EventsDAL.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsDAL.Models.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsDAL.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventsDAL.Models.Staff", b =>
                {
                    b.HasOne("EventsDAL.Models.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventsDAL.Models.TopicCovered", b =>
                {
                    b.HasOne("EventsDAL.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsDAL.Models.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsDAL.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
