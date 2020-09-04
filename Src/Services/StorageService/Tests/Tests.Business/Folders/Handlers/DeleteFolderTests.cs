using Common.Exceptions;
using MediatR;
using Moq;
using StorageService.Business.Commands.Folder.Delete;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Tests.Business.Folders.Handlers
{
    public class DeleteFolderTests
    {
        [Fact]
        public async Task Handle_Valid_Deletes()
        {
            // arrange
            var id = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            var request = new DeleteFolderCommand(id);
            var inputHierarchyToInsert = string.Empty;

            var repositoryMock = new Mock<IFolderRepository>();
            var insertSetup = repositoryMock.Setup(x => x.Delete(It.IsAny<string>(), CancellationToken.None));
            insertSetup.Callback((string hierarchy, CancellationToken token) => inputHierarchyToInsert = hierarchy);

            var mediatorMock = new Mock<IMediator>();
            var mediatorSetup = mediatorMock.SetupSequence(x => x.Send(It.IsAny<GetFolderQuery>(), CancellationToken.None));
            mediatorSetup.ReturnsAsync(
                new FolderDto()
                {
                    HierarchyId = "/Test/"
                }
            );
          

            var handler = new DeleteFolderCommandHandler(repositoryMock.Object, mediatorMock.Object);

            // act
            await handler.Handle(request, CancellationToken.None);


            // assert
            repositoryMock.Verify(x => x.Delete("/Test/", CancellationToken.None), Times.Exactly(1));
            repositoryMock.VerifyNoOtherCalls();
            Assert.Equal("/Test/", inputHierarchyToInsert);
        }

        [Fact]
        public async Task Handle_Invalid_Throws()
        {
            // arrange
            var id = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            var request = new DeleteFolderCommand(id);

            var repositoryMock = new Mock<IFolderRepository>();
            repositoryMock.Setup(x => x.Delete(It.IsAny<string>(), CancellationToken.None)).ThrowsAsync(new Exception());

            var mediatorMock = new Mock<IMediator>();
            var mediatorSetup = mediatorMock.Setup(x => x.Send(It.IsAny<GetFolderQuery>(), CancellationToken.None));
            mediatorSetup.ReturnsAsync(new FolderDto() {  });


            var handler = new DeleteFolderCommandHandler(repositoryMock.Object, mediatorMock.Object);

            // act
            await Assert.ThrowsAsync<DeleteException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            repositoryMock.Verify(x => x.Delete(It.IsAny<string>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
