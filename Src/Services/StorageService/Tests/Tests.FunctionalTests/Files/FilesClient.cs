using StorageService.API;
using StorageService.Business.Commands.File.Create;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests.FunctionalTests.Files
{
    public class FilesClient : ClientBase
    {
        public FilesClient(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        public async Task<HttpResponseMessage> Create(CreateFileCommand request)
        {
            var content = Utilities.GetRequestContent(request);
            return await _client.PostAsync($"api/File/", content);
        }

        public async Task<HttpResponseMessage> Search(string name, Guid? parentFolderId)
        {
            var url = new StringBuilder("/api/File/Search");

            if (!string.IsNullOrWhiteSpace(name) && parentFolderId != null)
            {

                url.Append($"?name={name}&parentFolderId={parentFolderId}");
            }
            else if(!string.IsNullOrWhiteSpace(name) && parentFolderId == null)
            {
                url.Append($"?name={name}");
            }
            else
            {
                url.Append($"?parentFolderId={parentFolderId}");
            }

            return await _client.GetAsync($"{url}");
        }


        public async Task<HttpResponseMessage> GetForFolder(Guid? parentFolderId)
        {
            var url = new StringBuilder("/api/File/GetForFolder");

            if(parentFolderId.HasValue)
            {
                url.Append($"&parentFolderId={parentFolderId}");
            }

            return await _client.GetAsync($"{url}");
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            return await _client.DeleteAsync($"/api/File/{id}");
        }

    }
}