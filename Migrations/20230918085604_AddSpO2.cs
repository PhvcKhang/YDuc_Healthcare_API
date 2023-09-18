using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddSpO2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpO2Id",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpO2s",
                columns: table => new
                {
                    SpO2Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpO2s", x => x.SpO2Id);
                    table.ForeignKey(
                        name: "FK_SpO2s_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SpO2Id",
                table: "Notifications",
                column: "SpO2Id",
                unique: true,
                filter: "[SpO2Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SpO2s_PersonId",
                table: "SpO2s",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_SpO2s_SpO2Id",
                table: "Notifications",
                column: "SpO2Id",
                principalTable: "SpO2s",
                principalColumn: "SpO2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_SpO2s_SpO2Id",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "SpO2s");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SpO2Id",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SpO2Id",
                table: "Notifications");
        }
    }
}
