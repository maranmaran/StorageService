using Common.Exceptions;
using MediatR;
using Moq;
using StorageService.Business.Commands.Folder.Create;
using StorageService.Business.Queries.Folder.Get;
using StorageService.Business.Queries.Folder.GetParentHierarchy;
using StorageService.Domain.Entities;
using StorageService.Persistence.DTOModels;
using StorageService.Persistence.Interfaces;
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
            };

            var mockRes = new Guid("a87887ec-df44-4607-9e6f-28f361670f9b");
            Folder folderToAssert = null;

            var repositoryMock = new Mock<IFolderRepository>();
            var insertSetup = repositoryMock.Setup(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None));
            insertSetup.ReturnsAsync(mockRes);
            insertSetup.Callback((Folder folder, CancellationToken _) => folderToAssert = folder);

            var _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetParentHierarchyQuery>(), CancellationToken.None))
                .ReturnsAsync("/Test/");

            var handler = new CreateFolderCommandHandler(repositoryMock.Object, AutomapperFactory.Get(), _mediatorMock.Object);

            // act
            var result = await handler.Handle(request, CancellationToken.None);

            // assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
            Assert.Equal(mockRes, result);
            Assert.Equal(folderToAssert.Name, request.Name);
            Assert.Equal(folderToAssert.HierarchyId, $"/Test/Test/");
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

            var _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetFolderQuery>(), CancellationToken.None)).ReturnsAsync(new FolderDto()
            {
                HierarchyId = "/Test/"
            });

            var handler = new CreateFolderCommandHandler(repositoryMock.Object, AutomapperFactory.Get(), _mediatorMock.Object);

            // act
            await Assert.ThrowsAsync<CreateException>(() => handler.Handle(request, CancellationToken.None));

            // assert
            repositoryMock.Verify(x => x.Insert(It.IsAny<Folder>(), CancellationToken.None), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
