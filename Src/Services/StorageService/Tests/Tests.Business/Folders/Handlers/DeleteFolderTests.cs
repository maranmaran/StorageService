using Common.Exceptions;
using StorageService.Business.Commands.Folder.Delete;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Domain.Entities;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
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
            var inputGuidToAssert = Guid.Empty;

            var repositoryMock = new Mock<IRepository<Folder>>();
            var insertSetup = repositoryMock.Setup(x => x.Delete(It.IsAny<Guid>(), CancellationToken.None));
            insertSetup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var transSetup = repositoryMock.Setup(x => x.CreateTransactionAsync(CancellationToken.None));
            

            var mediatorMock = new Mock<IMediator>();
            var mediatorSetup = mediatorMock.SetupSequence(x => x.Send(It.IsAny<GetFolderQuery>(), CancellationToken.None));
            mediatorSetup.ReturnsAsync(
                new FolderDto()
                {
                    Id = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b"),
                    Folders = new List<FolderDto>() {
                        new FolderDto()
                        {
                            Id = new Guid("b87887ec-df44-4607-9e6f-28f361670f9b")
                        }
                    }
                }
            );
            mediatorSetup.ReturnsAsync(
              new FolderDto()
              {
                  Id = new Guid("b87887ec-df44-4607-9e6f-28f361670f9b")
              }
            );

            var handler = new DeleteFolderCommandHandler(repositoryMock.Object, mediatorMock.Object);

            // act
            await handler.DeleteDependantChildren(request.Id, CancellationToken.None);

            var deletedIds = new[]
            {
                new Guid("b87887ec-df44-4607-9e6f-28f361670f9b"),
                new Guid("a87887ec-df44-4607-9e6f-28f361670f9b")
            };

            // assert
            repositoryMock.Verify(x => x.Delete(It.IsIn(deletedIds), CancellationToken.None), Times.Exactly(2));
            repositoryMock.VerifyNoOtherCalls();
            Assert.Equal(id, inputGuidToAssert);
        }

        [Fact]
        public async Task Handle_Invalid_Throws()
        {
            // arrange
            var id = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            var request = new DeleteFolderCommand(id);

            var repositoryMock = new Mock<IRepository<Folder>>();
            repositoryMock.Setup(x => x.Delete(It.IsAny<Guid>(), CancellationToken.None)).ThrowsAsync(new Exception());

            var mediatorMock = new Mock<IMediator>();
            var mediatorSetup = mediatorMock.Setup(x => x.Send(It.IsAny<GetFolderQuery>(), CancellationToken.None));
            mediatorSetup.ReturnsAsync(new FolderDto() {  });


            var handler = new DeleteFolderCommandHandler(repositoryMock.Object, mediatorMock.Object);

            // act
            await Assert.ThrowsAsync<DeleteException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            repositoryMock.Verify(x => x.Delete(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            repositoryMock.Verify(x =>x.CreateTransactionAsync(CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
