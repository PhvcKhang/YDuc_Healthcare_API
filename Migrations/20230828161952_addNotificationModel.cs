using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class addNotificationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificaitonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PatietnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorPersonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    seen = table.Column<bool>(type: "bit", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificaitonId);
                    table.ForeignKey(
                        name: "FK_Notification_Persons_DoctorPersonId",
                        column: x => x.DoctorPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_DoctorPersonId",
                table: "Notification",
                column: "DoctorPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
