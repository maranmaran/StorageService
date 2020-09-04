using Microsoft.SqlServer.Types;
using System;

namespace StorageService.Domain.Entities
{
    public class File : HierarchyHierarchyEntityBase
    {
        public string Name { get; set; }

        public Guid? ParentFolderId { get; set; }
        public virtual Folder ParentFolder { get; set; }
    }
}