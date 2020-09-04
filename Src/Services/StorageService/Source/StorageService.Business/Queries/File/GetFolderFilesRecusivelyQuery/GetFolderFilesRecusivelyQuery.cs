using MediatR;
using StorageService.Persistence.DTOModels;
using System;
using System.Collections.Generic;

namespace StorageService.Business.Queries.File.GetFolderFilesRecusivelyQuery
{
    /// <summary>
    /// Queries all files of particular folder in depth and across all nested folders.
    /// Handler: GetFolderFilesRecusivelyQueryHandler.cs
    /// </summary>
    public class GetFolderFilesRecusivelyQuery : IRequest<IEnumerable<FileDto>>
    {
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; }

        public GetFolderFilesRecusivelyQuery(Guid? parentFolderId, string name)
        {
            ParentFolderId = parentFolderId;
            Name = name;
        }
    }
}
