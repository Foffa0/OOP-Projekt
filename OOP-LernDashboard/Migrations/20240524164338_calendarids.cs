using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP_LernDashboard.Migrations
{
    /// <inheritdoc />
    public partial class calendarids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarIds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarIds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shortcuts_Name",
                table: "Shortcuts",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarIds");

            migrationBuilder.DropIndex(
                name: "IX_Shortcuts_Name",
                table: "Shortcuts");
        }
    }
}
