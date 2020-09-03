using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StorageService.Business.Commands.File.Create;
using StorageService.Business.Commands.File.Delete;
using StorageService.Business.Queries.File.GetFolderFiles;
using StorageService.Business.Queries.File.GetFolderFilesRecusivelyQuery;

namespace StorageService.API.Controllers
{
    /// <summary>
    /// Handles file management 
    /// </summary>
    public class FileController : BaseController
    {
        /// <summary>
        /// Searches for files inside folder structure
        /// </summary>
        /// <remarks>
        /// Retrieves all files if no query parameters are specified
        /// </remarks>
        /// <param name="name">Filters files by name by start with logic</param>
        /// <param name="parentFolderId">Defines which folder to query for files</param>
        [HttpGet("Search")]
        public async Task<IActionResult> GetFilesInsideFolderStructure([FromQuery] string name, [FromQuery] Guid? parentFolderId, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFolderFilesRecusivelyQuery(parentFolderId, name), cancellationToken));
        }

        /// <summary>
        /// Retrieves files inside folder
        /// </summary>
        /// <remarks>
        /// Retrieves all files for folder, retrieves root files if no query parameters are specified
        /// </remarks>
        [HttpGet("GetForFolder")]
        public async Task<IActionResult> GetForFolder([FromQuery] Guid? parentFolderId, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFolderFilesQuery(parentFolderId), cancellationToken));
        }

        /// <summary>
        /// Creates file
        /// </summary>
        /// <remarks>
        /// Creates file that can be placed on file
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFileCommand command, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <remarks>
        /// Deletes file
        /// Publishes event that file has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteFileCommand(id), cancellationToken));
        }
    }
}
