using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azin.Shadi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _mig_ProductGroup_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "ProductGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 12, 11, 55, 12, 432, DateTimeKind.Local).AddTicks(9159));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "ProductGroups");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 9, 7, 20, 36, 0, 956, DateTimeKind.Local).AddTicks(8154));
        }
    }
}
