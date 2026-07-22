using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changetablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PressureItems_PressureManagements_PressureManagementId",
                table: "PressureItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PressureManagements_IOModel_IOModelId",
                table: "PressureManagements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PressureManagements",
                table: "PressureManagements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PressureItems",
                table: "PressureItems");

            migrationBuilder.RenameTable(
                name: "PressureManagements",
                newName: "PressureManagement");

            migrationBuilder.RenameTable(
                name: "PressureItems",
                newName: "PressureItem");

            migrationBuilder.RenameIndex(
                name: "IX_PressureManagements_IOModelId",
                table: "PressureManagement",
                newName: "IX_PressureManagement_IOModelId");

            migrationBuilder.RenameIndex(
                name: "IX_PressureItems_PressureManagementId",
                table: "PressureItem",
                newName: "IX_PressureItem_PressureManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PressureManagement",
                table: "PressureManagement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PressureItem",
                table: "PressureItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PressureItem_PressureManagement_PressureManagementId",
                table: "PressureItem",
                column: "PressureManagementId",
                principalTable: "PressureManagement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PressureManagement_IOModel_IOModelId",
                table: "PressureManagement",
                column: "IOModelId",
                principalTable: "IOModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PressureItem_PressureManagement_PressureManagementId",
                table: "PressureItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PressureManagement_IOModel_IOModelId",
                table: "PressureManagement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PressureManagement",
                table: "PressureManagement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PressureItem",
                table: "PressureItem");

            migrationBuilder.RenameTable(
                name: "PressureManagement",
                newName: "PressureManagements");

            migrationBuilder.RenameTable(
                name: "PressureItem",
                newName: "PressureItems");

            migrationBuilder.RenameIndex(
                name: "IX_PressureManagement_IOModelId",
                table: "PressureManagements",
                newName: "IX_PressureManagements_IOModelId");

            migrationBuilder.RenameIndex(
                name: "IX_PressureItem_PressureManagementId",
                table: "PressureItems",
                newName: "IX_PressureItems_PressureManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PressureManagements",
                table: "PressureManagements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PressureItems",
                table: "PressureItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PressureItems_PressureManagements_PressureManagementId",
                table: "PressureItems",
                column: "PressureManagementId",
                principalTable: "PressureManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PressureManagements_IOModel_IOModelId",
                table: "PressureManagements",
                column: "IOModelId",
                principalTable: "IOModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
