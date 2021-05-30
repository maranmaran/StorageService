using System;

namespace StorageService.Domain.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
