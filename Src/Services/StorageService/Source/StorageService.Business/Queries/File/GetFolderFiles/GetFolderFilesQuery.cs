using System;
using System.Collections.Generic;
using MediatR;
using StorageService.Persistence.DTOModels;

namespace StorageService.Business.Queries.File.GetFolderFiles
{
    /// <summary>
    /// Queries all files of particular folder. On that level of folder.
    /// Handler: GetFolderFilesQueryHandler.cs
    /// </summary>
    public class GetFolderFilesQuery: IRequest<IEnumerable<FileDto>>
    {
        public GetFolderFilesQuery(Guid? parentFolderId)
        {
            ParentFolderId = parentFolderId;
        }

        public Guid? ParentFolderId { get; set; }
    }
}
