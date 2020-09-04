using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StorageService.Business.Queries.Folder.GetParentHierarchy;
using StorageService.Domain;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace StorageService.Business.Queries.Folder.GetContents
{
    internal class GetFolderContentsQueryHandler : IRequestHandler<GetFolderContentsQuery, IEnumerable<FolderDto>>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetFolderContentsQueryHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<FolderDto>> Handle(GetFolderContentsQuery request, CancellationToken cancellationToken)
        {
            // if id is null we are querying root folder... hence the delimiter sign
            var hierarchyId = await _mediator.Send(new GetParentHierarchyQuery(request.Id), cancellationToken);

            // get filter to query only this folder contents
            var filter = GetFilter(hierarchyId);

            var entities = await _repository.GetAll(
                filter,
                include: source => source.Include(x => x.Files), 
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<FolderDto>>(entities);
        }

        public Expression<Func<Domain.Entities.Folder, bool>> GetFilter(string hierarchyId)
        {
            // current folder contents are only relevant for one additional node hierarchies
            return PredicateBuilder.New<Domain.Entities.Folder>(file => file.HierarchyId == hierarchyId + file.Name + HierarchyHelper.Delimiter);

        }

    }

}