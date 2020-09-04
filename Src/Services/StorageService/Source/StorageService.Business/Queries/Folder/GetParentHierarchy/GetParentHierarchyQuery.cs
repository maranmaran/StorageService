using MediatR;
using System;

namespace StorageService.Business.Queries.Folder.GetParentHierarchy
{
    /// <summary>
    /// Queries for parent folder hierarchy data
    /// Handler: GetParentHierarchyQueryHandler.cs
    /// </summary>
    internal class GetParentHierarchyQuery: IRequest<string>
    {
        public GetParentHierarchyQuery(Guid? parentFolderId)
        {
            ParentFolderId = parentFolderId;
        }

        public Guid? ParentFolderId { get; set; }
    }
}
