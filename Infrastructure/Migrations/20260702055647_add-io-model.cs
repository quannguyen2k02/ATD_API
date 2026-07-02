using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addiomodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IO", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IOModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IOId = table.Column<int>(type: "int", nullable: true),
                    ModelName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IOModel_IO_IOId",
                        column: x => x.IOId,
                        principalTable: "IO",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IOConfigManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IOModelId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOConfigManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IOConfigManagement_IOModel_IOModelId",
                        column: x => x.IOModelId,
                        principalTable: "IOModel",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MotionPointsManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IOModelId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotionPointsManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotionPointsManagement_IOModel_IOModelId",
                        column: x => x.IOModelId,
                        principalTable: "IOModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OffsetManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IOModelId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffsetManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OffsetManagement_IOModel_IOModelId",
                        column: x => x.IOModelId,
                        principalTable: "IOModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IOConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IOConfigManagementId = table.Column<int>(type: "int", nullable: true),
                    Station = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cylinder = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Port = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Retest = table.Column<int>(type: "int", nullable: true),
                    LightSource1 = table.Column<int>(type: "int", nullable: true),
                    LightSource2 = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    TestPosition = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IOConfig_IOConfigManagement_IOConfigManagementId",
                        column: x => x.IOConfigManagementId,
                        principalTable: "IOConfigManagement",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MotionPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MotionPointsManagementId = table.Column<int>(type: "int", nullable: false),
                    LeftX = table.Column<double>(type: "double", nullable: false),
                    LeftY = table.Column<double>(type: "double", nullable: false),
                    LeftZ = table.Column<double>(type: "double", nullable: false),
                    RightX = table.Column<double>(type: "double", nullable: false),
                    RightY = table.Column<double>(type: "double", nullable: false),
                    RightZ = table.Column<double>(type: "double", nullable: false),
                    BackX = table.Column<double>(type: "double", nullable: false),
                    BackY = table.Column<double>(type: "double", nullable: false),
                    BackZ = table.Column<double>(type: "double", nullable: false),
                    HoldX = table.Column<double>(type: "double", nullable: false),
                    HoldY = table.Column<double>(type: "double", nullable: false),
                    HoldZ = table.Column<double>(type: "double", nullable: false),
                    TransY = table.Column<double>(type: "double", nullable: false),
                    MaxVel = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotionPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotionPoint_MotionPointsManagement_MotionPointsManagementId",
                        column: x => x.MotionPointsManagementId,
                        principalTable: "MotionPointsManagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Offset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OffsetManagementId = table.Column<int>(type: "int", nullable: false),
                    Module = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Port = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    X_Axis_Insertion = table.Column<double>(type: "double", nullable: true),
                    Y_Axis_Insertion = table.Column<double>(type: "double", nullable: true),
                    Z_Axis_Insertion = table.Column<double>(type: "double", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offset_OffsetManagement_OffsetManagementId",
                        column: x => x.OffsetManagementId,
                        principalTable: "OffsetManagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IOConfig_IOConfigManagementId",
                table: "IOConfig",
                column: "IOConfigManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_IOConfigManagement_IOModelId",
                table: "IOConfigManagement",
                column: "IOModelId");

            migrationBuilder.CreateIndex(
                name: "IX_IOModel_IOId",
                table: "IOModel",
                column: "IOId");

            migrationBuilder.CreateIndex(
                name: "IX_MotionPoint_MotionPointsManagementId",
                table: "MotionPoint",
                column: "MotionPointsManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_MotionPointsManagement_IOModelId",
                table: "MotionPointsManagement",
                column: "IOModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Offset_OffsetManagementId",
                table: "Offset",
                column: "OffsetManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_OffsetManagement_IOModelId",
                table: "OffsetManagement",
                column: "IOModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IOConfig");

            migrationBuilder.DropTable(
                name: "MotionPoint");

            migrationBuilder.DropTable(
                name: "Offset");

            migrationBuilder.DropTable(
                name: "IOConfigManagement");

            migrationBuilder.DropTable(
                name: "MotionPointsManagement");

            migrationBuilder.DropTable(
                name: "OffsetManagement");

            migrationBuilder.DropTable(
                name: "IOModel");

            migrationBuilder.DropTable(
                name: "IO");
        }
    }
}
