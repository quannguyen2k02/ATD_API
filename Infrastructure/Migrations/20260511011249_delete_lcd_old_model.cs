using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delete_lcd_old_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusDevice_KBs_KBId",
                table: "StatusDevice");

            migrationBuilder.AlterColumn<int>(
                name: "KBId",
                table: "StatusDevice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusDevice_KBs_KBId",
                table: "StatusDevice",
                column: "KBId",
                principalTable: "KBs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusDevice_KBs_KBId",
                table: "StatusDevice");

            migrationBuilder.AlterColumn<int>(
                name: "KBId",
                table: "StatusDevice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusDevice_KBs_KBId",
                table: "StatusDevice",
                column: "KBId",
                principalTable: "KBs",
                principalColumn: "Id");
        }
    }
}
