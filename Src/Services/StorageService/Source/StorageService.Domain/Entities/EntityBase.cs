using System;
using StorageService.Domain.Interfaces;

namespace StorageService.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}