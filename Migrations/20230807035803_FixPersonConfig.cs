using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixPersonConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPerson_Persons_PersonId",
                table: "PersonPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPerson_Persons_RelativesPersonId",
                table: "PersonPerson");

            migrationBuilder.DropTable(
                name: "HealthMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPerson",
                table: "PersonPerson");

            migrationBuilder.DropIndex(
                name: "IX_PersonPerson_RelativesPersonId",
                table: "PersonPerson");

            migrationBuilder.RenameColumn(
                name: "RelativesPersonId",
                table: "PersonPerson",
                newName: "PatientsPersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPerson",
                table: "PersonPerson",
                columns: new[] { "PatientsPersonId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPerson_PersonId",
                table: "PersonPerson",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPerson_Persons_PatientsPersonId",
                table: "PersonPerson",
                column: "PatientsPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPerson_Persons_PersonId",
                table: "PersonPerson",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPerson_Persons_PatientsPersonId",
                table: "PersonPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPerson_Persons_PersonId",
                table: "PersonPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPerson",
                table: "PersonPerson");

            migrationBuilder.DropIndex(
                name: "IX_PersonPerson_PersonId",
                table: "PersonPerson");

            migrationBuilder.RenameColumn(
                name: "PatientsPersonId",
                table: "PersonPerson",
                newName: "RelativesPersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPerson",
                table: "PersonPerson",
                columns: new[] { "PersonId", "RelativesPersonId" });

            migrationBuilder.CreateTable(
                name: "HealthMetrics",
                columns: table => new
                {
                    HealthMetricId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BloodSugar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BodyTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiastolicBloodPressure = table.Column<int>(type: "int", nullable: false),
                    PulseRate = table.Column<int>(type: "int", nullable: false),
                    SystolicBloodPressure = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetrics", x => x.HealthMetricId);
                    table.ForeignKey(
                        name: "FK_HealthMetrics_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPerson_RelativesPersonId",
                table: "PersonPerson",
                column: "RelativesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetrics_PersonId",
                table: "HealthMetrics",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPerson_Persons_PersonId",
                table: "PersonPerson",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPerson_Persons_RelativesPersonId",
                table: "PersonPerson",
                column: "RelativesPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
        }
    }
}
