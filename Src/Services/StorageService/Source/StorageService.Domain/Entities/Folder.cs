using System.Collections.Generic;

namespace StorageService.Domain.Entities
{
    public class Folder : HierarchyEntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<File> Files { get; set; } = new HashSet<File>();
    }
}
