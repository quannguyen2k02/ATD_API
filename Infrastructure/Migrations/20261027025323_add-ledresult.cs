using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addledresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caps",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Charge_White",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Charge_Yellow",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Close_Caps",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Close_F4",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Close_Num",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "F4",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "KBLight",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "KB_Close",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "N_Camera",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "LedResult");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "LedResult");

            migrationBuilder.RenameColumn(
                name: "TypeModel",
                table: "LedResult",
                newName: "FailItems");

            migrationBuilder.AlterColumn<string>(
                name: "CT",
                table: "LedResult",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FailItems",
                table: "LedResult",
                newName: "TypeModel");

            migrationBuilder.AlterColumn<int>(
                name: "CT",
                table: "LedResult",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Caps",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Charge_White",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Charge_Yellow",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Close_Caps",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Close_F4",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Close_Num",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "LedResult",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "F4",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "KBLight",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "KB_Close",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "N_Camera",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Num",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Power",
                table: "LedResult",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
