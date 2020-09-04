using Newtonsoft.Json;
using System;

namespace StorageService.Persistence.DTOModels
{
    public abstract class EntityDtoBase
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public string HierarchyId { get; set; }

    }
}