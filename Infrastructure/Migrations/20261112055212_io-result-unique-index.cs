using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ioresultuniqueindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IOResult_SN",
                table: "IOResult");

            migrationBuilder.CreateIndex(
                name: "IX_IOResult_SN",
                table: "IOResult",
                column: "SN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IOResult_SN",
                table: "IOResult");

            migrationBuilder.CreateIndex(
                name: "IX_IOResult_SN",
                table: "IOResult",
                column: "SN");
        }
    }
}
