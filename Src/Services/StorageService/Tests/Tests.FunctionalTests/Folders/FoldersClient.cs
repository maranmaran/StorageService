using StorageService.API;
using StorageService.Business.Commands.Folder.Create;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tests.FunctionalTests.Files;

namespace Tests.FunctionalTests.Folders
{
    public class FoldersClient : ClientBase
    {
        public FoldersClient(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        public async Task<HttpResponseMessage> Create(CreateFolderCommand request)
        {
            var content = Utilities.GetRequestContent(request);
            return await _client.PostAsync($"api/Folder/", content);
        }

        public async Task<HttpResponseMessage> GetContents(Guid? id)
        {
            var url = new StringBuilder("/api/Folder");

            if (id != null)
            {

                url.Append($"?id={id}");
            }
          
            return await _client.GetAsync($"{url}");
        }

        public async Task<HttpResponseMessage> Get(Guid id)
        {
            return await _client.GetAsync($"/api/Folder/{id}");
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            return await _client.DeleteAsync($"/api/Folder/{id}");
        }

    }
}