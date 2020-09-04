using Common.Exceptions;
using MediatR;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Persistence.Repositories;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using StorageService.Persistence.Interfaces;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace StorageService.Business.Commands.Folder.Delete
{
    internal class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Unit>
    {
        private readonly IFolderRepository _repository;
        private readonly IMediator _mediator;

        public DeleteFolderCommandHandler(IFolderRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var folder = await _mediator.Send(new GetFolderQuery(request.Id), cancellationToken);

                await _repository.Delete(folder.HierarchyId, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}