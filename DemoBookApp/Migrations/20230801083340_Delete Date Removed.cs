using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookApp.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDateRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Author",
                type: "datetime2",
                nullable: true);
        }
    }
}
