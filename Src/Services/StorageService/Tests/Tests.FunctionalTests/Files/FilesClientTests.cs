using StorageService.API;
using StorageService.Business.Commands.File.Create;
using StorageService.Persistence.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.FunctionalTests.Files
{
    public class FilesClientTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly FilesClient _client;

        public FilesClientTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = new FilesClient(factory);
        }

        [Fact]
        public async Task Get_FromRootAnyName_GetsAllFiles_StatusOk()
        {
            var response = await _client.Search(null, null);

            response.EnsureSuccessStatusCode();

            var data = await Utilities.GetResponseContent<IEnumerable<FileDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(10, data.Count());
        }

        [Fact]
        public async Task Get_FromRootAnyName_SearchFiles_StatusOk_RetrievesOnlySomeFiles()
        {
            var response = await _client.Search("File_Subfolder_", null);

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<IEnumerable<FileDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(6, data.Count());
        }

        [Fact]
        public async Task Get_FromRootAnyName_SearchFilesInFolder_StatusOk_RetrievesOnlySomeFiles()
        {
            var response = await _client.Search(null, new Guid("02e84ea4-3480-478F-AE03-2760935B19AC"));

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<IEnumerable<FileDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public async Task Create_AddToRoot_StatusOk()
        {
            var response = await _client.Create(new CreateFileCommand() { Name = "test", ParentFolderId = null });

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<Guid>(response);

            Assert.NotEqual(Guid.Empty, data);;
        }

        [Fact]
        public async Task Delete_Valid_StatusOk()
        {
            var response = await _client.Delete(new Guid("6AAEECA1-A5BD-409A-8722-0B904E2307A4"));

            response.EnsureSuccessStatusCode();
        }
    }
}
