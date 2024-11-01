using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfiniLore.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ff7761f-b907-493b-8b89-43b2ea6f644f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f443a138-c5da-4f16-abac-b55ae062c731");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserContentAccess<UniverseModel>",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserContentAccess<MultiverseModel>",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserContentAccess<LoreScopeModel>",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Universes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Multiverses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LoreScopes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9865437-ffc8-43d7-8580-ef4057c38859", null, "admin", "ADMIN" },
                    { "f1895849-07cb-4955-9660-1fcd7bd2ef22", null, "user", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1d02cb05-26b0-4ea1-97a3-2016b8abd112", "AQAAAAIAAYagAAAAEHTA2pbhIaT0nuuEXHtONtdoE4SKmXOupqoiRKCxggPkNyTg+8EA84MDCSrfLtAwvA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9865437-ffc8-43d7-8580-ef4057c38859");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1895849-07cb-4955-9660-1fcd7bd2ef22");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserContentAccess<UniverseModel>");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserContentAccess<MultiverseModel>");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserContentAccess<LoreScopeModel>");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Universes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Multiverses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LoreScopes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ff7761f-b907-493b-8b89-43b2ea6f644f", null, "admin", "ADMIN" },
                    { "f443a138-c5da-4f16-abac-b55ae062c731", null, "user", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "89bd417a-a110-4cf2-8a83-09dc79e47335", "AQAAAAIAAYagAAAAEB5p3i60R7of7y4lr9a8K13lTFUUhOYvDP6a38zvLQMYNCmfqaQ0KL2r9XF0KYX4sw==" });
        }
    }
}
