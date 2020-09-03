using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace StorageService.Business.Queries.Folder.Get
{
    internal class GetFolderQueryHandler : IRequestHandler<GetFolderQuery, FolderDto>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;

        public GetFolderQueryHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FolderDto> Handle(GetFolderQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(
                include: source => source
                    .Include(x => x.Folders)
                    .Include(x => x.Files),
                filter: file => file.Id == request.Id,
                cancellationToken: cancellationToken
            );

            return _mapper.Map<FolderDto>(entity);
        }
    }
}