using AutoMapper;
using Common.Exceptions;
using MediatR;
using StorageService.Business.Queries.Folder.GetParentHierarchy;
using StorageService.Domain;
using StorageService.Persistence.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace StorageService.Business.Commands.Folder.Create
{
    internal class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateFolderCommandHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Folder>(request);

                var parentHierarchy = await _mediator.Send(new GetParentHierarchyQuery(request.ParentFolderId), cancellationToken);
                entity.HierarchyId = $"{parentHierarchy}{entity.Name}{HierarchyHelper.Delimiter}";

                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }

       

    }
}