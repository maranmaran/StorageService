using MediatR;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StorageService.Business.Queries.Folder.GetParentHierarchy
{
    internal class GetParentHierarchyQueryHandler : IRequestHandler<GetParentHierarchyQuery, string>
    {
        private readonly IMediator _mediator;

        public GetParentHierarchyQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(GetParentHierarchyQuery request, CancellationToken cancellationToken)
        {
            if (request.ParentFolderId != null && request.ParentFolderId != Guid.Empty)
            {
                var folder = await _mediator.Send(new GetFolderQuery(request.ParentFolderId), cancellationToken);
                return folder.HierarchyId;
            }

            // default to root if no parent
            return HierarchyHelper.Delimiter;
        }
    }
}