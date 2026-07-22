using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixioline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IO_LineId",
                table: "IO",
                column: "LineId");

            migrationBuilder.AddForeignKey(
                name: "FK_IO_Line_LineId",
                table: "IO",
                column: "LineId",
                principalTable: "Line",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IO_Line_LineId",
                table: "IO");

            migrationBuilder.DropIndex(
                name: "IX_IO_LineId",
                table: "IO");
        }
    }
}
