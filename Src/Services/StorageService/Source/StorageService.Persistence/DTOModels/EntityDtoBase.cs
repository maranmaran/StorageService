using System;

namespace StorageService.Persistence.DTOModels
{
    public abstract class EntityDtoBase
    {
        public Guid Id { get; set; }
    }
}