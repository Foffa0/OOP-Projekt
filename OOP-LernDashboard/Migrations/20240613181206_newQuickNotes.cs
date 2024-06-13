using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP_LernDashboard.Migrations
{
    /// <inheritdoc />
    public partial class newQuickNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDateTime",
                table: "QuickNotes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentDateTime",
                table: "QuickNotes");
        }
    }
}
