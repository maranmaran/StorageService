using System;
using MediatR;
using StorageService.Persistence.DTOModels;

namespace StorageService.Business.Queries.Folder.Get
{
    public class GetFolderQuery : IRequest<FolderDto>
    {
        public Guid? Id { get; set; }

        public GetFolderQuery(Guid? id)
        {
            Id = id;
        }
    }
}
