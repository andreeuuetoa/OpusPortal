using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "LendedAt",
                table: "BookLentOut",
                newName: "LentAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LentAt",
                table: "BookLentOut",
                newName: "LendedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Person",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<char>(
                name: "Gender",
                table: "Person",
                type: "character(1)",
                nullable: true);
        }
    }
}
