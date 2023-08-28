using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPerson");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PersonId",
                table: "Notifications",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Persons_PersonId",
                table: "Notifications",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Persons_PersonId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PersonId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Notifications");

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
    }
}
