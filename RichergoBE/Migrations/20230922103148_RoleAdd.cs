using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RichergoBE.Migrations
{
    /// <inheritdoc />
    public partial class RoleAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9604011-e820-48a3-a3d1-35c1e9f887c3", "1", "Admin", "Admin" },
                    { "dee658f8-84c0-498b-9e38-be0b271b1540", "3", "User", "User" },
                    { "ece41f35-672c-4bad-a265-445e980a41d1", "2", "Manager", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9604011-e820-48a3-a3d1-35c1e9f887c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dee658f8-84c0-498b-9e38-be0b271b1540");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ece41f35-672c-4bad-a265-445e980a41d1");
        }
    }
}
