using System;
using MediatR;

namespace StorageService.Business.Commands.Folder.Delete
{
    /// <summary>
    /// Commands folder deletion
    /// Handler: DeleteFolderCommandHandler.cs
    /// </summary>
    public class DeleteFolderCommand : IRequest<Unit>
    {
        public DeleteFolderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
