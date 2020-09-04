using MediatR;
using StorageService.Persistence.DTOModels;
using System;
using System.Collections.Generic;

namespace StorageService.Business.Queries.Folder.GetContents
{
    /// <summary>
    /// Queries folder for it's folder children 
    /// Handler: GetFolderContentsQueryHandler.cs
    /// </summary>
    public class GetFolderContentsQuery : IRequest<IEnumerable<FolderDto>>
    {
        public Guid? Id { get; set; }

        public GetFolderContentsQuery(Guid? id)
        {
            Id = id;
        }
    }
}
