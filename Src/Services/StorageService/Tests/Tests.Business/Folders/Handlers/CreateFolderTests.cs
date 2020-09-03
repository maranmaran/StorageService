using Common.Exceptions;
using StorageService.Business.Commands.Folder.Create;
using StorageService.Domain.Entities;
using StorageService.Persistence.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Business.Folders.Handlers
{
    public class CreateFolderHandlerTests
    {
        [Fact]
        public async Task Handle_Valid_Inserts()
        {
            // arrange
            var request = new CreateFolderCommand()
            {
                Name = "Test",
                ParentFolderId = new Guid("97b7e004-be8e-424d-90c9-fa070996beb1")
            };

            var mockRes = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            Folder fileToAssert = null;

            var repositoryMock = new Mock<IRepository<Folder>>();
            var insertSetup = repositoryMock.Setup(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None));
            insertSetup.ReturnsAsync(mockRes);
            insertSetup.Callback((Folder file, CancellationToken _) => fileToAssert = file);

            var handler = new CreateFolderCommandHandler(repositoryMock.Object, AutomapperFactory.Get());

            // act
            var result = await handler.Handle(request, CancellationToken.None);

            // assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
            Assert.Equal(mockRes, result);
            Assert.Equal(fileToAssert.Name, request.Name);
            Assert.Equal(fileToAssert.ParentFolderId, request.ParentFolderId);
        }

        [Fact]
        public async Task Handle_Invalid_Throws()
        {
            // arrange
            var request = new CreateFolderCommand()
            {
                Name = "Test",
                ParentFolderId = new Guid("97b7e004-be8e-424d-90c9-fa070996beb1")
            };

            var repositoryMock = new Mock<IRepository<Folder>>();
            repositoryMock.Setup(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None)).ThrowsAsync(new Exception());

            var handler = new CreateFolderCommandHandler(repositoryMock.Object, AutomapperFactory.Get());

            // act
            await Assert.ThrowsAsync<CreateException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
