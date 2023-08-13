using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixHealthMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthMetrics_Persons_PersonId",
                table: "HealthMetrics");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "HealthMetrics",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "BloodPressures",
                columns: table => new
                {
                    BloodPressureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    PulseRate = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressures", x => x.BloodPressureId);
                    table.ForeignKey(
                        name: "FK_BloodPressures_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodSugars",
                columns: table => new
                {
                    BloodSugarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodSugars", x => x.BloodSugarId);
                    table.ForeignKey(
                        name: "FK_BloodSugars_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyTemperatures",
                columns: table => new
                {
                    BodyTemperatureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyTemperatures", x => x.BodyTemperatureId);
                    table.ForeignKey(
                        name: "FK_BodyTemperatures_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressures_PersonId",
                table: "BloodPressures",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodSugars_PersonId",
                table: "BloodSugars",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyTemperatures_PersonId",
                table: "BodyTemperatures",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthMetrics_Persons_PersonId",
                table: "HealthMetrics",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthMetrics_Persons_PersonId",
                table: "HealthMetrics");

            migrationBuilder.DropTable(
                name: "BloodPressures");

            migrationBuilder.DropTable(
                name: "BloodSugars");

            migrationBuilder.DropTable(
                name: "BodyTemperatures");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "HealthMetrics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthMetrics_Persons_PersonId",
                table: "HealthMetrics",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
