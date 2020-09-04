using System;
using MediatR;
using StorageService.Persistence.DTOModels;

namespace StorageService.Business.Queries.Folder.Get
{
    /// <summary>
    /// Queries folder data and it's files
    /// Handler: GetFolderQueryHandler.cs
    /// </summary>
    public class GetFolderQuery : IRequest<FolderDto>
    {
        public Guid? Id { get; set; }

        public GetFolderQuery(Guid? id)
        {
            Id = id;
        }
    }
}
