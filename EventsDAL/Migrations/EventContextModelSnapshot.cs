﻿// <auto-generated />
using System;
using EventsDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventsDAL.Migrations
{
    [DbContext(typeof(EventContext))]
    partial class EventContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                            EventId = new Guid("c1161c51-9731-4b24-8188-e9e45e8a7ea2"),
                            EventDate = new DateTime(2024, 6, 27, 14, 43, 29, 960, DateTimeKind.Local).AddTicks(6410),
                            EventDescription = "This is an annual event held by microsoft",
                            EventName = "Ignite"
                        },
                        new
                        {
                            EventId = new Guid("89b19ae5-39ad-49bd-8753-37d6f1110558"),
                            EventDate = new DateTime(2024, 6, 27, 14, 43, 29, 960, DateTimeKind.Local).AddTicks(6425),
                            EventDescription = "This is an annual event held by microsoft",
                            EventName = "Microsoft Build"
                        });
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
                            LocationId = new Guid("ff252c41-8faf-4dac-b6db-9f2d99d314ad"),
                            LocationName = "Chennai"
                        },
                        new
                        {
                            LocationId = new Guid("5957c463-248c-456f-89b7-e65bdfe13b96"),
                            LocationName = "Mumbai"
                        },
                        new
                        {
                            LocationId = new Guid("43b7455c-0092-4091-931c-8f5fd7135bf3"),
                            LocationName = "Seatle"
                        },
                        new
                        {
                            LocationId = new Guid("82ced29a-ec93-4001-92dc-0d11b286f327"),
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
