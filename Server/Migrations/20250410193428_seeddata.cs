using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Houses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Houses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceType",
                table: "Devices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                table: "Devices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "HouseId", "Address", "City", "Country", "Description", "Name", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0762f142-6ccb-4166-a049-f6c289447e6d"), "707 Rocky Path", "Marsa Alam", "Egypt", "Secluded cabin on a cliff", "Cliffside Cabin", "Red Sea Governorate", "99200" },
                    { new Guid("16a86fdb-2c84-4302-824e-a49c00e4369e"), "303 Sand Dunes Way", "Siwa", "Egypt", "Elegant villa in the desert oasis", "Desert Villa", "Matrouh Governorate", "99887" },
                    { new Guid("2fa0c930-3bdf-40a4-8d47-9ef458520c76"), "789 City Center", "Giza", "Egypt", "Modern apartment in downtown", "Urban Apartment", "Giza Governorate", "67890" },
                    { new Guid("57445249-2a18-4552-a929-d9e5949a7fb4"), "404 Shoreline Ave", "Luxor", "Egypt", "Charming house on the lake", "Lake House", "Luxor Governorate", "77665" },
                    { new Guid("648c36e6-cf4a-4686-a26e-a5b2605491c4"), "606 Oceanview Dr", "Hurghada", "Egypt", "Spacious villa by the sea", "Seaside Villa", "Red Sea Governorate", "22111" },
                    { new Guid("6963f81e-16b2-4168-84aa-49c65c7ec2fc"), "505 Heritage St", "Minya", "Egypt", "Grand manor with rich history", "Historic Manor", "Minya Governorate", "33445" },
                    { new Guid("95374554-afab-467f-b689-d53dc1ea92a5"), "101 Farm Road", "Fayoum", "Egypt", "Peaceful retreat in the countryside", "Country Cottage", "Fayoum Governorate", "11223" },
                    { new Guid("a05d91e7-556c-480b-88ab-d9c5da38bea0"), "202 Highland Blvd", "Aswan", "Egypt", "Rustic lodge with scenic views", "Mountain Lodge", "Aswan Governorate", "44556" },
                    { new Guid("d3b8a8f1-4c3b-4b8a-9c3b-4b8a9c3b4b8a"), "123 Palm Street", "Cairo", "Egypt", "Luxurious villa with pool", "Villa Oasis", "Cairo Governorate", "12345" },
                    { new Guid("e4c9b9f2-5d4c-5c9b-8d4c-5c9b8d4c5c9b"), "456 Beach Avenue", "Alexandria", "Egypt", "Cozy bungalow near the sea", "Sunset Bungalow", "Alexandria Governorate", "54321" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "DeviceName", "DeviceType", "HouseId", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("1511169f-8e5f-4847-b218-96bb8ee19ab5"), "Light Bulb", "Lighting", new Guid("a05d91e7-556c-480b-88ab-d9c5da38bea0"), "Active", null },
                    { new Guid("20d87e50-c4b7-4a48-982c-1b6c5c869c2c"), "Garage Door Opener", "Convenience", new Guid("648c36e6-cf4a-4686-a26e-a5b2605491c4"), "Inactive", null },
                    { new Guid("4a2d73b3-e951-403a-9149-4dd97db6cd2c"), "Water Leak Sensor", "Safety", new Guid("16a86fdb-2c84-4302-824e-a49c00e4369e"), "Inactive", null },
                    { new Guid("6270a75e-865d-4983-9f77-910aff4a7f57"), "Solar Panel Monitor", "Energy", new Guid("0762f142-6ccb-4166-a049-f6c289447e6d"), "Active", null },
                    { new Guid("7ba4c562-ab18-4b66-99d2-57cd0831a532"), "Smoke Detector", "Safety", new Guid("95374554-afab-467f-b689-d53dc1ea92a5"), "Active", null },
                    { new Guid("aff910da-bd8c-4dc5-9d04-a289a6c8e036"), "Smart Doorbell", "Access Control", new Guid("6963f81e-16b2-4168-84aa-49c65c7ec2fc"), "Active", null },
                    { new Guid("cb5bda8f-a6c4-4fbc-91b1-28a7bbb927f8"), "Motion Sensor", "Security", new Guid("57445249-2a18-4552-a929-d9e5949a7fb4"), "Active", null },
                    { new Guid("d017b91b-f958-46ba-be9b-c95a3769e217"), "Security Camera", "Camera", new Guid("e4c9b9f2-5d4c-5c9b-8d4c-5c9b8d4c5c9b"), "Active", null },
                    { new Guid("dd7c82c9-8b51-45a3-b470-d48a24096f34"), "Smart Lock", "Access Control", new Guid("2fa0c930-3bdf-40a4-8d47-9ef458520c76"), "Inactive", null },
                    { new Guid("f0e831cf-1223-4344-a8ff-2505a905c2b7"), "Thermostat", "Temperature", new Guid("d3b8a8f1-4c3b-4b8a-9c3b-4b8a9c3b4b8a"), "Active", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("1511169f-8e5f-4847-b218-96bb8ee19ab5"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("20d87e50-c4b7-4a48-982c-1b6c5c869c2c"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("4a2d73b3-e951-403a-9149-4dd97db6cd2c"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("6270a75e-865d-4983-9f77-910aff4a7f57"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("7ba4c562-ab18-4b66-99d2-57cd0831a532"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("aff910da-bd8c-4dc5-9d04-a289a6c8e036"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("cb5bda8f-a6c4-4fbc-91b1-28a7bbb927f8"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("d017b91b-f958-46ba-be9b-c95a3769e217"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("dd7c82c9-8b51-45a3-b470-d48a24096f34"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: new Guid("f0e831cf-1223-4344-a8ff-2505a905c2b7"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("0762f142-6ccb-4166-a049-f6c289447e6d"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("16a86fdb-2c84-4302-824e-a49c00e4369e"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("2fa0c930-3bdf-40a4-8d47-9ef458520c76"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("57445249-2a18-4552-a929-d9e5949a7fb4"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("648c36e6-cf4a-4686-a26e-a5b2605491c4"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("6963f81e-16b2-4168-84aa-49c65c7ec2fc"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("95374554-afab-467f-b689-d53dc1ea92a5"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("a05d91e7-556c-480b-88ab-d9c5da38bea0"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("d3b8a8f1-4c3b-4b8a-9c3b-4b8a9c3b4b8a"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "HouseId",
                keyValue: new Guid("e4c9b9f2-5d4c-5c9b-8d4c-5c9b8d4c5c9b"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceType",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
