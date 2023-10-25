using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class OneNotificationToManyStatics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_BloodPressureId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BloodSugarId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BodyTemperatureId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SpO2Id",
                table: "Notifications");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BloodPressureId",
                table: "Notifications",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BloodSugarId",
                table: "Notifications",
                column: "BloodSugarId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BodyTemperatureId",
                table: "Notifications",
                column: "BodyTemperatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SpO2Id",
                table: "Notifications",
                column: "SpO2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_BloodPressureId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BloodSugarId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BodyTemperatureId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SpO2Id",
                table: "Notifications");

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SpO2Id",
                table: "Notifications",
                column: "SpO2Id",
                unique: true,
                filter: "[SpO2Id] IS NOT NULL");
        }
    }
}
