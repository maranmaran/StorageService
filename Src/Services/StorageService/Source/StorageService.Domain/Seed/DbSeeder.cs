using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StorageService.Domain.Entities;

namespace StorageService.Domain.Seed
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedFolders();
            builder.SeedFiles();
        }

        public static void Seed(this ApplicationDbContext context)
        {
            context.SeedFolders();
            context.SeedFiles();
        }

        private static IEnumerable<Folder> GetFolders()
        {
            return new List<Folder>()
            {
                #region Folder structure A
                new Folder()
                {
                    Id = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Folder_A",
                },
                new Folder()
                {
                    Id = new Guid("02e84ea4-3480-478f-ae03-2760935b19ac"),
                    Name = "Subfolder_A_1",
                    ParentFolderId = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d")
                },
                new Folder()
                {
                    Id = new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26"),
                    Name = "Subfolder_Subfolder_A_1",
                    ParentFolderId = new Guid("02e84ea4-3480-478f-ae03-2760935b19ac")
                },
                new Folder()
                {
                    Id = new Guid("042d9c67-1f99-4cf0-a307-c91f74896905"),
                    Name = "Subfolder_A_2",
                    ParentFolderId = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d")
                },
                #endregion

                #region Folder structure B
                new Folder()
                {
                    Id = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Folder_B",
                },
                new Folder()
                {
                    Id = new Guid("12e84ea4-3480-478f-ae03-2760935b19ac"),
                    Name = "Subfolder_B_1",
                    ParentFolderId = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d")
                },
                new Folder()
                {
                    Id = new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26"),
                    Name = "Subfolder_Subfolder_B_1",
                    ParentFolderId = new Guid("12e84ea4-3480-478f-ae03-2760935b19ac")
                },
                new Folder()
                {
                    Id = new Guid("142d9c67-1f99-4cf0-a307-c91f74896905"),
                    Name = "Subfolder_B_2",
                    ParentFolderId = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d")
                },
                #endregion

                #region Folder structure C
                new Folder()
                {
                    Id = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Folder_C",
                },
                new Folder()
                {
                    Id = new Guid("22e84ea4-3480-478f-ae03-2760935b19ac"),
                    Name = "Subfolder_C_1",
                    ParentFolderId = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d")
                },
                new Folder()
                {
                    Id = new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26"),
                    Name = "Subfolder_Subfolder_C_1",
                    ParentFolderId = new Guid("22e84ea4-3480-478f-ae03-2760935b19ac")
                },
                new Folder()
                {
                    Id = new Guid("242d9c67-1f99-4cf0-a307-c91f74896905"),
                    Name = "Subfolder_C_2",
                    ParentFolderId = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d")
                },
                #endregion

            };
        }
        private static void SeedFolders(this ModelBuilder builder)
        {
            builder.Entity<Folder>().HasData(GetFolders());
        }
        private static void SeedFolders(this ApplicationDbContext context)
        {
            context.Folders.AddRange(GetFolders());
            context.SaveChanges();
        }

        private static IEnumerable<File> GetFiles()
        {
            return new List<File>()
            {
                #region Root files
                new File()
                {
                    Id = new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"),
                    Name = "File_Root",
                },
                #endregion

                #region Folder structure A files
                new File()
                {
                    Id = new Guid("0e2f0009-f8ac-4dbd-961a-b89adedec3c7"),
                    Name = "File_Folder_A",
                    ParentFolderId = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d")
                },
                new File()
                {
                    Id = new Guid("c272feac-ac31-4a09-b73c-0d3305ba721c"),
                    Name = "File_Subfolder_A",
                    ParentFolderId = new Guid("02e84ea4-3480-478f-ae03-2760935b19ac")
                },
                new File()
                {
                    Id = new Guid("4aaeeca1-a5bd-409a-8722-0b904e2307a4"),
                    Name = "File_Subfolder_Subfolder_A",
                    ParentFolderId = new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26")
                },
                #endregion 

                #region Folder structure B files
                new File()
                {
                    Id = new Guid("1e2f0009-f8ac-4dbd-961a-b89adedec3c7"),
                    Name = "File_Folder_B",
                    ParentFolderId = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d")
                },
                new File()
                {
                    Id = new Guid("d272feac-ac31-4a09-b73c-0d3305ba721c"),
                    Name = "File_Subfolder_B",
                    ParentFolderId = new Guid("12e84ea4-3480-478f-ae03-2760935b19ac")
                },
                new File()
                {
                    Id = new Guid("5aaeeca1-a5bd-409a-8722-0b904e2307a4"),
                    Name = "File_Subfolder_Subfolder_B",
                    ParentFolderId = new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26")
                },
                #endregion

                #region Folder structure C files
                new File()
                {
                    Id = new Guid("2e2f0009-f8ac-4dbd-961a-b89adedec3c7"),
                    Name = "File_Folder_C",
                    ParentFolderId = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d")
                },
                new File()
                {
                    Id = new Guid("e272feac-ac31-4a09-b73c-0d3305ba721c"),
                    Name = "File_Subfolder_C",
                    ParentFolderId = new Guid("22e84ea4-3480-478f-ae03-2760935b19ac")
                },
                new File()
                {
                    Id = new Guid("6aaeeca1-a5bd-409a-8722-0b904e2307a4"),
                    Name = "File_Subfolder_Subfolder_C",
                    ParentFolderId = new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26")
                },
                #endregion
            };

        }
        private static void SeedFiles(this ModelBuilder builder)
        {
            builder.Entity<File>().HasData(GetFiles());
        }
        private static void SeedFiles(this ApplicationDbContext context)
        {
            context.AddRange(GetFiles());
        }
    }
}
