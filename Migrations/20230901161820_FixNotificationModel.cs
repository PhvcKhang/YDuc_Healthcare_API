using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixNotificationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seen",
                table: "Notifications",
                newName: "Seen");

            migrationBuilder.RenameColumn(
                name: "PatietnId",
                table: "Notifications",
                newName: "PatientName");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Seen",
                table: "Notifications",
                newName: "seen");

            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "Notifications",
                newName: "PatietnId");
        }
    }
}
