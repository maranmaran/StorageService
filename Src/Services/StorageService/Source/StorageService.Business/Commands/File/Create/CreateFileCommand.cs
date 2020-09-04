using System;
using MediatR;

namespace StorageService.Business.Commands.File.Create
{

    /// <summary>
    /// Commands file creation
    /// Handler: CreateFileCommandHandler.cs
    /// </summary>
    public class CreateFileCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentFolderId { get; set; }

        public CreateFileCommand(string name, Guid? parentFolderId)
        {
            Name = name;
            ParentFolderId = parentFolderId;
        }

        public CreateFileCommand()
        {
            
        }
    }
}
