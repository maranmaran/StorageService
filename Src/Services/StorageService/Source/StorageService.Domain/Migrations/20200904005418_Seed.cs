using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StorageService.Domain.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "HierarchyId", "Name", "ParentFolderId" },
                values: new object[] { new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"), "/File_Root/", "File_Root", null });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "HierarchyId", "Name" },
                values: new object[,]
                {
                    { new Guid("0138a704-604e-4ebf-8da4-2271be9a953d"), "/Folder_A/", "Folder_A" },
                    { new Guid("02e84ea4-3480-478f-ae03-2760935b19ac"), "/Folder_A/Subfolder_A_1/", "Subfolder_A_1" },
                    { new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26"), "/Folder_A/Subfolder_A_1/Subfolder_Subfolder_A_1/", "Subfolder_Subfolder_A_1" },
                    { new Guid("042d9c67-1f99-4cf0-a307-c91f74896905"), "/Folder_A/Subfolder_A_2/", "Subfolder_A_2" },
                    { new Guid("1138a704-604e-4ebf-8da4-2271be9a953d"), "/Folder_B/", "Folder_B" },
                    { new Guid("12e84ea4-3480-478f-ae03-2760935b19ac"), "/Folder_B/Subfolder_B_1/", "Subfolder_B_1" },
                    { new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26"), "/Folder_B/Subfolder_B_1/Subfolder_Subfolder_B_1/", "Subfolder_Subfolder_B_1" },
                    { new Guid("142d9c67-1f99-4cf0-a307-c91f74896905"), "/Folder_B/Subfolder_B_2/", "Subfolder_B_2" },
                    { new Guid("2138a704-604e-4ebf-8da4-2271be9a953d"), "/Folder_C/", "Folder_C" },
                    { new Guid("22e84ea4-3480-478f-ae03-2760935b19ac"), "/Folder_C/Subfolder_C_1/", "Subfolder_C_1" },
                    { new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26"), "/Folder_C/Subfolder_C_1/Subfolder_Subfolder_C_1/", "Subfolder_Subfolder_C_1" },
                    { new Guid("242d9c67-1f99-4cf0-a307-c91f74896905"), "/Folder_C/Subfolder_C_2/", "Subfolder_C_2" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "HierarchyId", "Name", "ParentFolderId" },
                values: new object[,]
                {
                    { new Guid("0e2f0009-f8ac-4dbd-961a-b89adedec3c7"), "/Folder_A/File_Folder_A/", "File_Folder_A", new Guid("0138a704-604e-4ebf-8da4-2271be9a953d") },
                    { new Guid("c272feac-ac31-4a09-b73c-0d3305ba721c"), "/Folder_A/Subfolder_A_1/File_Subfolder_A/", "File_Subfolder_A", new Guid("02e84ea4-3480-478f-ae03-2760935b19ac") },
                    { new Guid("4aaeeca1-a5bd-409a-8722-0b904e2307a4"), "/Folder_A/Subfolder_A_1/Subfolder_Subfolder_A_1/File_Subfolder_Subfolder_A/", "File_Subfolder_Subfolder_A", new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26") },
                    { new Guid("1e2f0009-f8ac-4dbd-961a-b89adedec3c7"), "/Folder_B/File_Folder_B/", "File_Folder_B", new Guid("1138a704-604e-4ebf-8da4-2271be9a953d") },
                    { new Guid("d272feac-ac31-4a09-b73c-0d3305ba721c"), "/Folder_B/Subfolder_B_1/File_Subfolder_B/", "File_Subfolder_B", new Guid("12e84ea4-3480-478f-ae03-2760935b19ac") },
                    { new Guid("5aaeeca1-a5bd-409a-8722-0b904e2307a4"), "/Folder_B/Subfolder_B_1/Subfolder_Subfolder_B_1/File_Subfolder_Subfolder_B/", "File_Subfolder_Subfolder_B", new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26") },
                    { new Guid("2e2f0009-f8ac-4dbd-961a-b89adedec3c7"), "/Folder_C/File_Folder_C/", "File_Folder_C", new Guid("2138a704-604e-4ebf-8da4-2271be9a953d") },
                    { new Guid("e272feac-ac31-4a09-b73c-0d3305ba721c"), "/Folder_C/Subfolder_C_1/File_Subfolder_C/", "File_Subfolder_C", new Guid("22e84ea4-3480-478f-ae03-2760935b19ac") },
                    { new Guid("6aaeeca1-a5bd-409a-8722-0b904e2307a4"), "/Folder_C/Subfolder_C_1/Subfolder_Subfolder_C_1/File_Subfolder_Subfolder_C/", "File_Subfolder_Subfolder_C", new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("0e2f0009-f8ac-4dbd-961a-b89adedec3c7"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("1e2f0009-f8ac-4dbd-961a-b89adedec3c7"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("2e2f0009-f8ac-4dbd-961a-b89adedec3c7"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("4aaeeca1-a5bd-409a-8722-0b904e2307a4"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("5aaeeca1-a5bd-409a-8722-0b904e2307a4"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("6aaeeca1-a5bd-409a-8722-0b904e2307a4"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("c272feac-ac31-4a09-b73c-0d3305ba721c"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("d272feac-ac31-4a09-b73c-0d3305ba721c"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("e272feac-ac31-4a09-b73c-0d3305ba721c"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("042d9c67-1f99-4cf0-a307-c91f74896905"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("142d9c67-1f99-4cf0-a307-c91f74896905"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("242d9c67-1f99-4cf0-a307-c91f74896905"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("0138a704-604e-4ebf-8da4-2271be9a953d"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("02e84ea4-3480-478f-ae03-2760935b19ac"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("1138a704-604e-4ebf-8da4-2271be9a953d"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("12e84ea4-3480-478f-ae03-2760935b19ac"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("2138a704-604e-4ebf-8da4-2271be9a953d"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("22e84ea4-3480-478f-ae03-2760935b19ac"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26"));
        }
    }
}
