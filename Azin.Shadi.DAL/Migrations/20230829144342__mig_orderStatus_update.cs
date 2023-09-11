using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azin.Shadi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _mig_orderStatus_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "OrderStatuses",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 8, 29, 18, 13, 42, 343, DateTimeKind.Local).AddTicks(4764));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderStatuses",
                newName: "OrderStatusId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 8, 29, 18, 3, 47, 439, DateTimeKind.Local).AddTicks(7833));
        }
    }
}
