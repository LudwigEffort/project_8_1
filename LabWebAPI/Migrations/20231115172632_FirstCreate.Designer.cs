﻿// <auto-generated />
using System;
using LabWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LabWebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231115172632_FirstCreate")]
    partial class FirstCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("LabWebAPI.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemIdentifier")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TechSpec")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LabWebAPI.Model.ItemSoftware", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId", "SoftwareId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("ItemSoftwares");
                });

            modelBuilder.Entity("LabWebAPI.Model.LabUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LabUsers");
                });

            modelBuilder.Entity("LabWebAPI.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LabUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReservationStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("LabUserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("LabWebAPI.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("LabWebAPI.Model.Software", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SoftwareName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("LabWebAPI.Model.Item", b =>
                {
                    b.HasOne("LabWebAPI.Model.Room", "Room")
                        .WithMany("Items")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("LabWebAPI.Model.ItemSoftware", b =>
                {
                    b.HasOne("LabWebAPI.Model.Item", "Item")
                        .WithMany("ItemSoftwares")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LabWebAPI.Model.Software", "Software")
                        .WithMany("ItemSoftwares")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Software");
                });

            modelBuilder.Entity("LabWebAPI.Model.Reservation", b =>
                {
                    b.HasOne("LabWebAPI.Model.Item", "Item")
                        .WithMany("Reservations")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LabWebAPI.Model.LabUser", "LabUser")
                        .WithMany("Reservations")
                        .HasForeignKey("LabUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("LabUser");
                });

            modelBuilder.Entity("LabWebAPI.Model.Item", b =>
                {
                    b.Navigation("ItemSoftwares");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("LabWebAPI.Model.LabUser", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("LabWebAPI.Model.Room", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LabWebAPI.Model.Software", b =>
                {
                    b.Navigation("ItemSoftwares");
                });
#pragma warning restore 612, 618
        }
    }
}
