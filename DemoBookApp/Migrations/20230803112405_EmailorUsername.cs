using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookApp.Migrations
{
    /// <inheritdoc />
    public partial class EmailorUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserList",
                newName: "emailOrUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "emailOrUsername",
                table: "UserList",
                newName: "Name");
        }
    }
}
