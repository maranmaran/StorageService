using System;
using System.Collections.Generic;
using MediatR;
using StorageService.Persistence.DTOModels;

namespace StorageService.Business.Queries.Folder.GetContents
{
    public class GetFolderContentsQuery : IRequest<IEnumerable<FolderDto>>
    {
        public Guid? Id { get; set; }

        public GetFolderContentsQuery(Guid? id)
        {
            Id = id;
        }
    }
}
