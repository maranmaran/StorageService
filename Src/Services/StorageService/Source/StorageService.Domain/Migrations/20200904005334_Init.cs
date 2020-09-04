using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StorageService.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HierarchyId = table.Column<string>(nullable: true, defaultValue: "/"),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HierarchyId = table.Column<string>(nullable: true, defaultValue: "/"),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    ParentFolderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Folders_ParentFolderId",
                        column: x => x.ParentFolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_HierarchyId",
                table: "Files",
                column: "HierarchyId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ParentFolderId",
                table: "Files",
                column: "ParentFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Name_ParentFolderId",
                table: "Files",
                columns: new[] { "Name", "ParentFolderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_HierarchyId",
                table: "Folders",
                column: "HierarchyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
