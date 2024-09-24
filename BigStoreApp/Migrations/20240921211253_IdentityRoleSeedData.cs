using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BigStoreApp.Migrations
{
    /// <inheritdoc />
    public partial class IdentityRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c4df198-b14c-4358-92b6-5a791628fbad", "e6693041-489c-4c8a-8cb4-5bd9f95f5906", "User", "USER" },
                    { "99fe8d9d-6b10-4ec8-88b1-d854cf982ae0", "611a254e-a49e-49bf-a39a-6c7954cd0498", "Editor", "EDITOR" },
                    { "e3de914c-c7ec-466b-b4dd-1fe205bf41eb", "f9346af6-2e8e-49af-ad3e-55190dff0f75", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 21, 17, 12, 53, 543, DateTimeKind.Local).AddTicks(2124));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 21, 17, 12, 53, 543, DateTimeKind.Local).AddTicks(2164));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 12, 53, 543, DateTimeKind.Local).AddTicks(2642), "_photos/sabun.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 12, 53, 543, DateTimeKind.Local).AddTicks(2649), "_photos/parfum.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 12, 53, 543, DateTimeKind.Local).AddTicks(2651), "_photos/ruj.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 12, 53, 543, DateTimeKind.Local).AddTicks(2653), "_photos/monitor.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c4df198-b14c-4358-92b6-5a791628fbad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99fe8d9d-6b10-4ec8-88b1-d854cf982ae0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3de914c-c7ec-466b-b4dd-1fe205bf41eb");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 21, 17, 3, 27, 347, DateTimeKind.Local).AddTicks(1023));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 21, 17, 3, 27, 347, DateTimeKind.Local).AddTicks(1059));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 3, 27, 347, DateTimeKind.Local).AddTicks(1389), "sabun.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 3, 27, 347, DateTimeKind.Local).AddTicks(1395), "parfum.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 3, 27, 347, DateTimeKind.Local).AddTicks(1398), "ruj.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 9, 21, 17, 3, 27, 347, DateTimeKind.Local).AddTicks(1400), "monitor.jpg" });
        }
    }
}
