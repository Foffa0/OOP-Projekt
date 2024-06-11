using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP_LernDashboard.Migrations
{
    /// <inheritdoc />
    public partial class Timer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourInput",
                table: "Timers");

            migrationBuilder.RenameColumn(
                name: "SecondInput",
                table: "Timers",
                newName: "isPaused");

            migrationBuilder.RenameColumn(
                name: "MinuteInput",
                table: "Timers",
                newName: "TickSize");

            migrationBuilder.AddColumn<double>(
                name: "ElapsedTime",
                table: "Timers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Timers",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<double>(
                name: "TotalTime",
                table: "Timers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElapsedTime",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "TotalTime",
                table: "Timers");

            migrationBuilder.RenameColumn(
                name: "isPaused",
                table: "Timers",
                newName: "SecondInput");

            migrationBuilder.RenameColumn(
                name: "TickSize",
                table: "Timers",
                newName: "MinuteInput");

            migrationBuilder.AddColumn<int>(
                name: "HourInput",
                table: "Timers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
