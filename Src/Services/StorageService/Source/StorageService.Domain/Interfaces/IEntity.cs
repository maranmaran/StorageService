using System;

namespace StorageService.Domain.Interfaces
{
    public interface IHierarchyEntity
    {
        public Guid Id { get; set; }
        public string HierarchyId { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
