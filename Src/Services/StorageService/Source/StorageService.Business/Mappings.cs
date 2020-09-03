using AutoMapper;
using StorageService.Business.Commands.File.Create;
using StorageService.Business.Commands.Folder.Create;
using StorageService.Domain.Entities;
using StorageService.Persistence.DTOModels;

namespace StorageService.Business
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CreateFileCommand, File>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
            
            CreateMap<CreateFolderCommand, Folder>().ReverseMap();
            CreateMap<Folder, FolderDto>().ReverseMap();
        }
    }
}
