using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixOne2OneRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_BloodPressures_BloodPressureId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_BloodSugars_BloodSugarId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_BodyTemperatures_BodyTemperatureId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BloodPressureId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BloodSugarId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BodyTemperatureId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "BloodPressureId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "BloodSugarId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "BodyTemperatureId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "NotificationId",
                table: "BodyTemperatures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationId",
                table: "BloodSugars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationId",
                table: "BloodPressures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyTemperatures_NotificationId",
                table: "BodyTemperatures",
                column: "NotificationId",
                unique: true,
                filter: "[NotificationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BloodSugars_NotificationId",
                table: "BloodSugars",
                column: "NotificationId",
                unique: true,
                filter: "[NotificationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressures_NotificationId",
                table: "BloodPressures",
                column: "NotificationId",
                unique: true,
                filter: "[NotificationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodPressures_Notifications_NotificationId",
                table: "BloodPressures",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodSugars_Notifications_NotificationId",
                table: "BloodSugars",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatures_Notifications_NotificationId",
                table: "BodyTemperatures",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodPressures_Notifications_NotificationId",
                table: "BloodPressures");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodSugars_Notifications_NotificationId",
                table: "BloodSugars");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatures_Notifications_NotificationId",
                table: "BodyTemperatures");

            migrationBuilder.DropIndex(
                name: "IX_BodyTemperatures_NotificationId",
                table: "BodyTemperatures");

            migrationBuilder.DropIndex(
                name: "IX_BloodSugars_NotificationId",
                table: "BloodSugars");

            migrationBuilder.DropIndex(
                name: "IX_BloodPressures_NotificationId",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "BloodSugars");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "BloodPressures");

            migrationBuilder.AddColumn<string>(
                name: "BloodPressureId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodSugarId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyTemperatureId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BloodPressureId",
                table: "Notifications",
                column: "BloodPressureId",
                unique: true,
                filter: "[BloodPressureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BloodSugarId",
                table: "Notifications",
                column: "BloodSugarId",
                unique: true,
                filter: "[BloodSugarId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BodyTemperatureId",
                table: "Notifications",
                column: "BodyTemperatureId",
                unique: true,
                filter: "[BodyTemperatureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_BloodPressures_BloodPressureId",
                table: "Notifications",
                column: "BloodPressureId",
                principalTable: "BloodPressures",
                principalColumn: "BloodPressureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_BloodSugars_BloodSugarId",
                table: "Notifications",
                column: "BloodSugarId",
                principalTable: "BloodSugars",
                principalColumn: "BloodSugarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_BodyTemperatures_BodyTemperatureId",
                table: "Notifications",
                column: "BodyTemperatureId",
                principalTable: "BodyTemperatures",
                principalColumn: "BodyTemperatureId");
        }
    }
}
