using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using MediatR;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Persistence.Interfaces;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace StorageService.Business.Commands.Folder.Delete
{
    internal class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Unit>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMediator _mediator;

        public DeleteFolderCommandHandler(IRepository<Domain.Entities.Folder> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var transaction = await _repository.CreateTransactionAsync(cancellationToken);
                
                await DeleteDependantChildren(request.Id, cancellationToken);

                await transaction.CommitAsync(cancellationToken);                

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }

        /// <summary>
        /// Recursively delete all dependant children folders
        /// </summary>
        internal async Task DeleteDependantChildren(Guid id, CancellationToken cancellationToken)
        {
            var folder = await _mediator.Send(new GetFolderQuery(id), cancellationToken);
            var children = folder.Folders;

            foreach (var child in children)
            {
                await DeleteDependantChildren(child.Id, cancellationToken);
            }
            
            await _repository.Delete(id, cancellationToken);
        }
    }
}