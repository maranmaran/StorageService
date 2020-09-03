using Common.Exceptions;
using StorageService.Business.Commands.File.Delete;
using StorageService.Domain.Entities;
using StorageService.Persistence.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Business.Files.Handlers
{
    public class DeleteFileHandlerTests
    {
        [Fact]
        public async Task Handle_Valid_Deletes()
        {
            // arrange
            var id = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            var request = new DeleteFileCommand(id);
            var inputGuidToAssert = Guid.Empty;

            var repositoryMock = new Mock<IRepository<File>>();
            var insertSetup = repositoryMock.Setup(x => x.Delete(It.IsAny<Guid>(), CancellationToken.None));
            insertSetup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var handler = new DeleteFileCommandHandler(repositoryMock.Object);

            // act
           await handler.Handle(request, CancellationToken.None);

            // assert
            repositoryMock.Verify(x => x.Delete(id, CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
            Assert.Equal(id, inputGuidToAssert);
        }

        [Fact]
        public async Task Handle_Invalid_Throws()
        {
            // arrange
            var id = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            var request = new DeleteFileCommand(id);

            var repositoryMock = new Mock<IRepository<File>>();
            repositoryMock.Setup(x => x.Delete(It.IsAny<Guid>(), CancellationToken.None)).ThrowsAsync(new Exception());

            var handler = new DeleteFileCommandHandler(repositoryMock.Object);

            // act
            await Assert.ThrowsAsync<DeleteException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            repositoryMock.Verify(x => x.Delete(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
