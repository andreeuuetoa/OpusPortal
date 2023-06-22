using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAppRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshToken_AspNetUsers_AppUserId",
                table: "AppRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRefreshToken",
                table: "AppRefreshToken");

            migrationBuilder.RenameTable(
                name: "AppRefreshToken",
                newName: "AppRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_AppRefreshToken_AppUserId",
                table: "AppRefreshTokens",
                newName: "IX_AppRefreshTokens_AppUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PreviousExpirationDateTime",
                table: "AppRefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousRefreshToken",
                table: "AppRefreshTokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRefreshTokens",
                table: "AppRefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRefreshTokens",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "PreviousExpirationDateTime",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "PreviousRefreshToken",
                table: "AppRefreshTokens");

            migrationBuilder.RenameTable(
                name: "AppRefreshTokens",
                newName: "AppRefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_AppRefreshTokens_AppUserId",
                table: "AppRefreshToken",
                newName: "IX_AppRefreshToken_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRefreshToken",
                table: "AppRefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshToken_AspNetUsers_AppUserId",
                table: "AppRefreshToken",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
