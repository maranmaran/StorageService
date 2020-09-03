using System;

namespace StorageService.Domain.Entities
{
    public class File : EntityBase
    {
        public string Name { get; set; }

        public Guid? ParentFolderId { get; set; }
        public virtual Folder ParentFolder { get; set; }
    }
}