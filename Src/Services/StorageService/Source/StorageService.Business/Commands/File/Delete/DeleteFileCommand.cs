using System;
using MediatR;

namespace StorageService.Business.Commands.File.Delete
{
    /// <summary>
    /// Commands file deletion
    /// Handler: DeleteFileCommandHandler.cs
    /// </summary>
    public class DeleteFileCommand : IRequest<Unit>
    {
        public DeleteFileCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
