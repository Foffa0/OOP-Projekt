﻿using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.DbContexts
{
    internal class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ToDoDTO> ToDos { get; set; }

        public DbSet<ShortcutDTO> Shortcuts { get; set; }

        /// <summary>
        /// Make shure that the Name of the Shortcut is unique
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortcutDTO>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}
