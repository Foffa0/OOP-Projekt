using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP_LernDashboard.Migrations
{
    /// <inheritdoc />
    public partial class countdownNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Notification",
                table: "Countdowns",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notification",
                table: "Countdowns");
        }
    }
}
