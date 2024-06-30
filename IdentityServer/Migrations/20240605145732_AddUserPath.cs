using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cf042ec-8269-4133-8d34-cb3f84986bb7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e62f1910-66b4-4c5a-84cb-3c0859857260");

            migrationBuilder.AddColumn<string>(
                name: "UserPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "99c70703-1064-4420-aa66-f349d7e805d1", null, "User", "USER" },
                    { "c612f706-d8b9-41ff-a305-13ba5bd7f083", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99c70703-1064-4420-aa66-f349d7e805d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c612f706-d8b9-41ff-a305-13ba5bd7f083");

            migrationBuilder.DropColumn(
                name: "UserPath",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5cf042ec-8269-4133-8d34-cb3f84986bb7", null, "User", "USER" },
                    { "e62f1910-66b4-4c5a-84cb-3c0859857260", null, "Admin", "ADMIN" }
                });
        }
    }
}
