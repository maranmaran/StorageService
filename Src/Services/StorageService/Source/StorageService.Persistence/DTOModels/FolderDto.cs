using System.Collections.Generic;

namespace StorageService.Persistence.DTOModels
{
    public class FolderDto : EntityDtoBase
    {
        public string Name { get; set; }

        public IEnumerable<FileDto> Files { get; set; } = new List<FileDto>();
        public IEnumerable<FolderDto> Folders { get; set; } = new List<FolderDto>();
    }
}
