using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class removeNotificaiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "ImageLink",
                table: "BodyTemperatures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageLink",
                table: "BloodSugars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageLink",
                table: "BloodPressures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageLink",
                table: "BodyTemperatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageLink",
                table: "BloodSugars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageLink",
                table: "BloodPressures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificaitonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Heading = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificaitonId);
                    table.ForeignKey(
                        name: "FK_Notifications_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PersonId",
                table: "Notifications",
                column: "PersonId");
        }
    }
}
