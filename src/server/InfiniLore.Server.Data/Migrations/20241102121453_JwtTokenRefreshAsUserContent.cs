using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfiniLore.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class JwtTokenRefreshAsUserContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JwtRefreshTokens_AspNetUsers_UserId",
                table: "JwtRefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9865437-ffc8-43d7-8580-ef4057c38859");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1895849-07cb-4955-9660-1fcd7bd2ef22");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JwtRefreshTokens",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_JwtRefreshTokens_UserId",
                table: "JwtRefreshTokens",
                newName: "IX_JwtRefreshTokens_OwnerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "JwtRefreshTokens",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "JwtRefreshTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "JwtRefreshTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoftDeleteDate",
                table: "JwtRefreshTokens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserContentAccess<JwtRefreshTokenModel>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    JwtRefreshTokenModelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SoftDeleteDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContentAccess<JwtRefreshTokenModel>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContentAccess<JwtRefreshTokenModel>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserContentAccess<JwtRefreshTokenModel>_JwtRefreshTokens_JwtRefreshTokenModelId",
                        column: x => x.JwtRefreshTokenModelId,
                        principalTable: "JwtRefreshTokens",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23ceb702-2b6c-42b5-878c-3da746245683", null, "admin", "ADMIN" },
                    { "caf179c8-c7f8-4175-beec-507caada22d1", null, "user", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65cda2bf-7ee0-4afe-8791-7b413e24d875", "AQAAAAIAAYagAAAAEClySXudZt2aYzQCMlJBKSN+hxJE5Y1vG1eLoYgzVbYbZbPGKPbvMydo4LHyzA80PQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserContentAccess<JwtRefreshTokenModel>_JwtRefreshTokenModelId",
                table: "UserContentAccess<JwtRefreshTokenModel>",
                column: "JwtRefreshTokenModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContentAccess<JwtRefreshTokenModel>_UserId",
                table: "UserContentAccess<JwtRefreshTokenModel>",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JwtRefreshTokens_AspNetUsers_OwnerId",
                table: "JwtRefreshTokens",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JwtRefreshTokens_AspNetUsers_OwnerId",
                table: "JwtRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserContentAccess<JwtRefreshTokenModel>");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23ceb702-2b6c-42b5-878c-3da746245683");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caf179c8-c7f8-4175-beec-507caada22d1");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "JwtRefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "JwtRefreshTokens");

            migrationBuilder.DropColumn(
                name: "SoftDeleteDate",
                table: "JwtRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "JwtRefreshTokens",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_JwtRefreshTokens_OwnerId",
                table: "JwtRefreshTokens",
                newName: "IX_JwtRefreshTokens_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "JwtRefreshTokens",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_JwtRefreshTokens_AspNetUsers_UserId",
                table: "JwtRefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
