using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookApp.Migrations
{
    /// <inheritdoc />
    public partial class ProperUserTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserList",
                table: "UserList");

            migrationBuilder.RenameTable(
                name: "UserList",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserList",
                table: "UserList",
                column: "userId");
        }
    }
}
