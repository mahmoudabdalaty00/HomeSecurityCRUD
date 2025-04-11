using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AlarmsPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TriggerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggeredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFalseAlarm = table.Column<bool>(type: "bit", nullable: false),
                    AutoResponseTriggered = table.Column<bool>(type: "bit", nullable: false),
                    HouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId");
                    table.ForeignKey(
                        name: "FK_Alarms_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId");
                    table.ForeignKey(
                        name: "FK_Alarms_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_DeviceId",
                table: "Alarms",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_HouseId",
                table: "Alarms",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_NotificationId",
                table: "Alarms",
                column: "NotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");
        }
    }
}
