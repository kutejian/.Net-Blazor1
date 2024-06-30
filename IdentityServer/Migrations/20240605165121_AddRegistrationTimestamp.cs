using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistrationTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99c70703-1064-4420-aa66-f349d7e805d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c612f706-d8b9-41ff-a305-13ba5bd7f083");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTimestamp",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d593173-af06-4a4c-9526-81c598ffb9d3", null, "Admin", "ADMIN" },
                    { "369c6282-b8b5-45eb-ae32-7e50908e91c4", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d593173-af06-4a4c-9526-81c598ffb9d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "369c6282-b8b5-45eb-ae32-7e50908e91c4");

            migrationBuilder.DropColumn(
                name: "RegistrationTimestamp",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "99c70703-1064-4420-aa66-f349d7e805d1", null, "User", "USER" },
                    { "c612f706-d8b9-41ff-a305-13ba5bd7f083", null, "Admin", "ADMIN" }
                });
        }
    }
}
