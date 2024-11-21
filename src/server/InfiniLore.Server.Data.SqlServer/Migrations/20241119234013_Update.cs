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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "921dae24-9b6e-4687-b641-099923fbbbec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fea0722b-84d3-4929-854b-c76b2ababaa2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2352a1cc-342a-42e4-9dca-3004fc9b075c", null, "user", "USER" },
                    { "e8ce5ced-3c52-4677-a650-c52de2cd7727", null, "admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3977386f-eb7b-4c12-b435-19ee021ee5f5", "AQAAAAIAAYagAAAAEGi84KGOoV/HPPk5GHyGbVia1dZWfEQzrCDbqkQ6H3R1/EiyZDCZ6RzVeibtBVEuTQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2352a1cc-342a-42e4-9dca-3004fc9b075c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8ce5ced-3c52-4677-a650-c52de2cd7727");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "921dae24-9b6e-4687-b641-099923fbbbec", null, "admin", "ADMIN" },
                    { "fea0722b-84d3-4929-854b-c76b2ababaa2", null, "user", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b927c8ed-0c99-4c48-88bf-5a3f640cf138", "AQAAAAIAAYagAAAAENiEn6B0+N6v0oJ3Nmx1hOr8LyCLm05ifM9exetGypqNCVxolZ1S2qw6GGTgtqZ7QQ==" });
        }
    }
}
