using AutoMapper;
using LinqKit;
using MediatR;
using StorageService.Business.Queries.Folder.GetParentHierarchy;
using StorageService.Business.Settings;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace StorageService.Business.Queries.File.GetFolderFilesRecusivelyQuery
{
    internal class GetFolderFilesRecusivelyQueryHandler : IRequestHandler<GetFolderFilesRecusivelyQuery, IEnumerable<FileDto>>
    {
        private readonly IRepository<Domain.Entities.File> _repository;
        private readonly IMapper _mapper;
        private readonly AppSettings _settings;
        private readonly IMediator _mediator;

        public GetFolderFilesRecusivelyQueryHandler(IRepository<Domain.Entities.File> repository, IMapper mapper, AppSettings settings, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _settings = settings;
            _mediator = mediator;
        }

        public async Task<IEnumerable<FileDto>> Handle(GetFolderFilesRecusivelyQuery request, CancellationToken cancellationToken)
        {
            // get hierarchy
            var hierarchyId = await _mediator.Send(new GetParentHierarchyQuery(request.ParentFolderId), cancellationToken);
         
            // build filter to query data
            var filter = GetFilter(hierarchyId, request.Name);
            
            // retrieve data
            var files = await _repository.GetAll(filter, cancellationToken: cancellationToken);
            
            // limit data
            var limitedFiles = files.Take(_settings.FilesQueryLimit);

            // return
            return _mapper.Map<IEnumerable<FileDto>>(limitedFiles);
        }

        /// <summary>
        /// Builds filter for filtering files by hierarchy and name in startsWith fashion or retrieve all if null
        /// </summary>
        public Expression<Func<Domain.Entities.File, bool>> GetFilter(string hierarchyId, string nameQuery)
        {
            // hierarchy query is default
            var predicate = PredicateBuilder.New<Domain.Entities.File>(x => x.HierarchyId.StartsWith(hierarchyId));

            // query by name only if we have any query. Otherwise retrieve all results
            if (!string.IsNullOrWhiteSpace(nameQuery))
            {
                predicate.And(x => x.Name.StartsWith(nameQuery));
            }

            return predicate;
        }
    }

}