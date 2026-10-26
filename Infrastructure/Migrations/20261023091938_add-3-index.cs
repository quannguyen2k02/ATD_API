using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add3index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReplacementRecord_IOName_ReplaceDate",
                table: "ReplacementRecord");

            migrationBuilder.AlterColumn<string>(
                name: "Port",
                table: "ReplacementRecord",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReplacementRecord_IOName_Port_ReplaceDate",
                table: "ReplacementRecord",
                columns: new[] { "IOName", "Port", "ReplaceDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReplacementRecord_IOName_Port_ReplaceDate",
                table: "ReplacementRecord");

            migrationBuilder.AlterColumn<string>(
                name: "Port",
                table: "ReplacementRecord",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReplacementRecord_IOName_ReplaceDate",
                table: "ReplacementRecord",
                columns: new[] { "IOName", "ReplaceDate" },
                unique: true);
        }
    }
}
