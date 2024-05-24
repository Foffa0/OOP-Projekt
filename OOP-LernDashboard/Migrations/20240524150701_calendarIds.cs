using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP_LernDashboard.Migrations
{
    /// <inheritdoc />
    public partial class calendarIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Calendars",
                table: "Calendars");

            migrationBuilder.RenameTable(
                name: "Calendars",
                newName: "CalendarIds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarIds",
                table: "CalendarIds",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarIds",
                table: "CalendarIds");

            migrationBuilder.RenameTable(
                name: "CalendarIds",
                newName: "Calendars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calendars",
                table: "Calendars",
                column: "Id");
        }
    }
}
