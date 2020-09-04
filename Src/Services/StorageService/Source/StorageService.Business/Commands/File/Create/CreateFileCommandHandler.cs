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
namespace StorageService.Business.Commands.File.Create
{
    internal class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.File> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateFileCommandHandler(IRepository<Domain.Entities.File> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.File>(request);

                // build new hierarchy on top of parent
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