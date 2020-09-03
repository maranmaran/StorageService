using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StorageService.Business.Commands.Folder.Create;
using StorageService.Business.Commands.Folder.Delete;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Business.Queries.Folder.GetContents;

namespace StorageService.API.Controllers
{
    /// <summary>
    /// Hanldes folder management
    /// </summary>
    public class FolderController : BaseController
    {
        /// <summary>
        /// Gets folder 
        /// </summary>
        /// <remarks>
        /// Retrieves folder information and contents
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFolderQuery(id), cancellationToken));
        }

        /// <summary>
        /// Gets folder contents
        /// </summary>
        /// <remarks>
        /// Retrieves folder contents, for root id is null
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetContents([FromQuery] Guid? id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFolderContentsQuery(id), cancellationToken));
        }

        /// <summary>
        /// Creates folder
        /// </summary>
        /// <remarks>
        /// Creates folder that can be placed on folder
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFolderCommand command, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Delete folder
        /// </summary>
        /// <remarks>
        /// Deletes folder
        /// Publishes event that folder has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteFolderCommand(id), cancellationToken));
        }
    }
}