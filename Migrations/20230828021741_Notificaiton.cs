using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class Notificaiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificaitonId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Heading = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificaitonId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationPerson",
                columns: table => new
                {
                    NotificationsNotificaitonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PeoplePersonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPerson", x => new { x.NotificationsNotificaitonId, x.PeoplePersonId });
                    table.ForeignKey(
                        name: "FK_NotificationPerson_Notifications_NotificationsNotificaitonId",
                        column: x => x.NotificationsNotificaitonId,
                        principalTable: "Notifications",
                        principalColumn: "NotificaitonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationPerson_Persons_PeoplePersonId",
                        column: x => x.PeoplePersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPerson_PeoplePersonId",
                table: "NotificationPerson",
                column: "PeoplePersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPerson");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
