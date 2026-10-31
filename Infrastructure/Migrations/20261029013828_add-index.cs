using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Tạo index composite (LEDId, DateTime) TRƯỚC
            //    → composite index này có thể phục vụ FK trên cột LEDId
            migrationBuilder.CreateIndex(
                name: "IX_LedResult_LEDId_DateTime",
                table: "LedResult",
                columns: new[] { "LEDId", "DateTime" });

            // 2) Giờ mới được xóa index cũ (đã có index mới thay thế)
            migrationBuilder.DropIndex(
                name: "IX_LedResult_LEDId",
                table: "LedResult");

            // 3) Các AlterColumn và index còn lại
            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "LedResult",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "LedResult",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LedResult_DateTime",
                table: "LedResult",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LedResult_ModelName_DateTime",
                table: "LedResult",
                columns: new[] { "ModelName", "DateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_LedResult_Result",
                table: "LedResult",
                column: "Result");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1) Tạo lại index cũ TRƯỚC (để phục vụ FK)
            migrationBuilder.CreateIndex(
                name: "IX_LedResult_LEDId",
                table: "LedResult",
                column: "LEDId");

            // 2) Giờ mới xóa index composite
            migrationBuilder.DropIndex(
                name: "IX_LedResult_LEDId_DateTime",
                table: "LedResult");

            // 3) Các bước còn lại
            migrationBuilder.DropIndex(
                name: "IX_LedResult_DateTime",
                table: "LedResult");

            migrationBuilder.DropIndex(
                name: "IX_LedResult_ModelName_DateTime",
                table: "LedResult");

            migrationBuilder.DropIndex(
                name: "IX_LedResult_Result",
                table: "LedResult");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "LedResult",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "LedResult",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}