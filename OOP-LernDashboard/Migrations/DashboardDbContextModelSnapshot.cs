﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OOP_LernDashboard.DbContexts;

#nullable disable

namespace OOP_LernDashboard.Migrations
{
    [DbContext(typeof(DashboardDbContext))]
    partial class DashboardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.18");

            modelBuilder.Entity("OOP_LernDashboard.DTOs.CalendarDTO", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CalendarIds");
                });

            modelBuilder.Entity("OOP_LernDashboard.DTOs.CountdownDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Notification")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Countdowns");
                });

            modelBuilder.Entity("OOP_LernDashboard.DTOs.QuickNoteDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("QuickNotes");
                });

            modelBuilder.Entity("OOP_LernDashboard.DTOs.ShortcutDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("IconPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Shortcuts");
                });

            modelBuilder.Entity("OOP_LernDashboard.DTOs.TimerDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("ElapsedTime")
                        .HasColumnType("REAL");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TickSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalTime")
                        .HasColumnType("REAL");

                    b.Property<bool>("isPaused")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Timers");
                });

            modelBuilder.Entity("OOP_LernDashboard.DTOs.ToDoDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("IntervalTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRecurringToDo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToDos");
                });
#pragma warning restore 612, 618
        }
    }
}
