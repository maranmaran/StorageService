using Common.Exceptions;
using StorageService.Business.Commands.File.Create;
using StorageService.Domain.Entities;
using StorageService.Persistence.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Business.Files.Handlers
{
    public class CreateFileHandlerTests
    {
        [Fact]
        public async Task Handle_Valid_Inserts()
        {
            // arrange
            var request = new CreateFileCommand()
            {
                Name = "Test",
                ParentFolderId = new Guid("97b7e004-be8e-424d-90c9-fa070996beb1")
            };

            var mockRes = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            File fileToAssert = null;

            var repositoryMock = new Mock<IRepository<File>>();
            var insertSetup = repositoryMock.Setup(x => x.Insert(It.IsAny<File>(), CancellationToken.None));
            insertSetup.ReturnsAsync(mockRes);
            insertSetup.Callback((File file, CancellationToken _) => fileToAssert = file);

            var handler = new CreateFileCommandHandler(repositoryMock.Object, AutomapperFactory.Get());
            
            // act
            var result = await handler.Handle(request, CancellationToken.None);
            
            // assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<File>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
            Assert.Equal(mockRes, result);
            Assert.Equal(fileToAssert.Name, request.Name);
            Assert.Equal(fileToAssert.ParentFolderId, request.ParentFolderId);
        }

        [Fact]
        public async Task Handle_Invalid_Throws()
        {
            // arrange
            var request = new CreateFileCommand()
            {
                Name = "Test",
                ParentFolderId = new Guid("97b7e004-be8e-424d-90c9-fa070996beb1")
            };

            var repositoryMock = new Mock<IRepository<File>>();
            repositoryMock.Setup(x => x.Insert(It.IsAny<File>(), CancellationToken.None)).ThrowsAsync(new Exception());

            var handler = new CreateFileCommandHandler(repositoryMock.Object, AutomapperFactory.Get());

            // act
            await Assert.ThrowsAsync<CreateException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<File>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
