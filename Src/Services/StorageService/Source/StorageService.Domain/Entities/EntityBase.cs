using StorageService.Domain.Interfaces;
using System;

namespace StorageService.Domain.Entities
{
    public abstract class HierarchyEntityBase : IHierarchyEntity
    {
        public Guid Id { get; set; }
        public string HierarchyId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}