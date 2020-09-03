using StorageService.API;
using StorageService.Business.Commands.Folder.Create;
using StorageService.Persistence.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.FunctionalTests.Folders
{
    public class FoldersClientTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly FoldersClient _client;

        public FoldersClientTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = new FoldersClient(factory);
        }

        [Fact]
        public async Task Get_SearchSpecificFolder_GetSingleFolder_StatusOk()
        {
            var response = await _client.Get(new Guid("0138A704-604E-4EBF-8DA4-2271BE9A953D"));

            response.EnsureSuccessStatusCode();

            var data = await Utilities.GetResponseContent<FolderDto>(response);

            Assert.NotNull(data);
            Assert.Equal(data.Id, new Guid("0138A704-604E-4EBF-8DA4-2271BE9A953D"));
        }

        [Fact]
        public async Task Get_GetRootContents_StatusOk()
        {
            var response = await _client.GetContents(null);

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<IEnumerable<FolderDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public async Task Get_GetSpecificFolderContents_StatusOk()
        {
            var response = await _client.GetContents(new Guid("0138a704-604e-4ebf-8da4-2271be9a953d"));

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<IEnumerable<FolderDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(2, data.Count());
        }


        [Fact]
        public async Task Create_AddToRoot_StatusOk()
        {
            var response = await _client.Create(new CreateFolderCommand() { Name = "test", ParentFolderId = null });

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<Guid>(response);

            Assert.NotEqual(Guid.Empty, data);;
        }

        [Fact]
        public async Task Delete_Valid_StatusOk()
        {
            var response = await _client.Delete(new Guid("1138a704-604e-4ebf-8da4-2271be9a953d"));

            response.EnsureSuccessStatusCode();
        }
    }
}
