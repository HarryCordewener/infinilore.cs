using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfiniLore.Server.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1c82784a-88b6-4344-9a08-a0acf2662696");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "80946331-3c7d-4181-b7fa-18381cf48693");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7612850e-8c21-47b3-bb00-9ac8ff819675", "AQAAAAIAAYagAAAAEOFvgjGb2UE6tZmwyjNuowOSntKvI0gbeelY4Pkq6u5wm3NF7ceEOc2moP2R5lIC2w==" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c3bc5e0-4c80-4d26-8bef-f3bf7a357751", null, "admin", "ADMIN" },
                    { "12981bf7-16b7-4441-8754-42c0a5bf8df2", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0c3bc5e0-4c80-4d26-8bef-f3bf7a357751");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "12981bf7-16b7-4441-8754-42c0a5bf8df2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "15743b7e-cc0b-4a22-a48d-bc163433bc58", "AQAAAAIAAYagAAAAEO56MotVjoAxBm3AmnPHgFADjTihjYTVngLnj+OoNqDON/ev6G2dxPi8iVbi5rsfFw==" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c82784a-88b6-4344-9a08-a0acf2662696", null, "user", "USER" },
                    { "80946331-3c7d-4181-b7fa-18381cf48693", null, "admin", "ADMIN" }
                });
        }
    }
}
