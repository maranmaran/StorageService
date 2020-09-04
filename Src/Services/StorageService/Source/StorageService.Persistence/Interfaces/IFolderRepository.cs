using System.Threading;
using System.Threading.Tasks;
using StorageService.Domain.Entities;

namespace StorageService.Persistence.Interfaces
{
    public interface IFolderRepository : IRepository<Folder>
    {
        Task Delete(string hierarchyId, CancellationToken cancellationToken);
    }
}