using System;
using MediatR;

namespace StorageService.Business.Commands.Folder.Create
{
    public class CreateFolderCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentFolderId { get; set; }

        public CreateFolderCommand(string name, Guid? parentFolderId)
        {
            Name = name;
            ParentFolderId = parentFolderId;
        }

        public CreateFolderCommand()
        {
            
        }
    }
}
