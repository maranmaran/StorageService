using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using MediatR;
using StorageService.Business.Queries.File.GetFolderFiles;
using StorageService.Business.Queries.Folder.GetContents;
using StorageService.Business.Settings;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;

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
            return await SearchFilesRecursively(
                nameQuery: GetNameFilter(request.Name), 
                parentFolderId: request.ParentFolderId, 
                limit: _settings.FilesQueryLimit, 
                cancellationToken: cancellationToken
           );
        }

        /// <summary>
        /// Recursively delete all dependant children folders
        /// </summary>
        private async Task<IEnumerable<FileDto>> SearchFilesRecursively(Expression<Func<FileDto, bool>> nameQuery, Guid? parentFolderId, int limit,  CancellationToken cancellationToken)
        {
            // get current level folder files
            var folderFiles = await _mediator.Send(new GetFolderFilesQuery(parentFolderId), cancellationToken);

            // do search
            var files = folderFiles
                .Where(nameQuery.Compile())
                .Take(limit)
                .ToList();

            // assert limitations
            if (files.Count >= limit)
                return files.Take(limit);

            limit -= files.Count;

            // go on recursively for all children folders until you have hit the limitation if you're clear 
            var childFoldersToCheck = await _mediator.Send(new GetFolderContentsQuery(parentFolderId), cancellationToken);
            foreach (var child in childFoldersToCheck)
            {
                var childFiles = (await SearchFilesRecursively(nameQuery, child.Id, limit, cancellationToken)).ToList();

                if (childFiles.Count >= limit)
                {
                    files.AddRange(childFiles.Take(limit));
                    break;
                }

                limit -= childFiles.Count;

                files.AddRange(childFiles);
            }

            return files;
        }

        /// <summary>
        /// Builds name filter
        /// </summary>
        private Expression<Func<FileDto, bool>> GetNameFilter(string nameQuery)
        {
            var predicate = PredicateBuilder.New<FileDto>(true);

            if(!string.IsNullOrWhiteSpace(nameQuery))
            {
                predicate.And(f => f.Name.StartsWith(nameQuery));
            }

            return predicate;
        }
    }

}