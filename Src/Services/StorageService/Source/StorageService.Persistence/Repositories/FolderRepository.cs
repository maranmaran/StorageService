using StorageService.Domain;
using StorageService.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StorageService.Persistence.Interfaces;

namespace StorageService.Persistence.Repositories
{
    internal class FolderRepository: Repository<Folder>, IFolderRepository
    {
        private readonly ApplicationDbContext _context;

        public FolderRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Delete(string hierarchyId, CancellationToken cancellationToken)
        {
            var candidatesToDelete = _context.Folders.Where(x => x.HierarchyId.StartsWith(hierarchyId));

            _context.Folders.RemoveRange(candidatesToDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
