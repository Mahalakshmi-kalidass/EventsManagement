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
    [Migration("20240722115354_add_user_table")]
    partial class add_user_table
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
                            EventId = new Guid("13b0daea-89ac-4376-832c-7c0862b38cf9"),
                            EventDate = new DateTime(2024, 7, 22, 17, 23, 53, 896, DateTimeKind.Local).AddTicks(7893),
                            EventDescription = "This is an annual event held by microsoft",
                            EventName = "Ignite"
                        },
                        new
                        {
                            EventId = new Guid("56d5d83e-9cd0-4df2-8780-80c2a34ad848"),
                            EventDate = new DateTime(2024, 7, 22, 17, 23, 53, 896, DateTimeKind.Local).AddTicks(7913),
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
                            LocationId = new Guid("0854e7ba-00b3-4825-b5ff-4935ec0f5f14"),
                            LocationName = "Chennai"
                        },
                        new
                        {
                            LocationId = new Guid("a4bf9da2-de7d-41cf-89ce-e170cd3e1436"),
                            LocationName = "Mumbai"
                        },
                        new
                        {
                            LocationId = new Guid("d93ee7a7-bccd-4690-8ca3-993eb6e81b56"),
                            LocationName = "Seatle"
                        },
                        new
                        {
                            LocationId = new Guid("a6955010-0972-401d-b44a-a04b95209447"),
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

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
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
