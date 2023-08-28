using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Persons_DoctorPersonId",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_DoctorPersonId",
                table: "Notifications",
                newName: "IX_Notifications_DoctorPersonId");


            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificaitonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Persons_DoctorPersonId",
                table: "Notifications",
                column: "DoctorPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Persons_DoctorPersonId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_DoctorPersonId",
                table: "Notification",
                newName: "IX_Notification_DoctorPersonId");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "NotificaitonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Persons_DoctorPersonId",
                table: "Notification",
                column: "DoctorPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
        }
    }
}
