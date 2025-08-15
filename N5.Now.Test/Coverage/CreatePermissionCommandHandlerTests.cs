using Moq;
using N5.Now.Application.Commands;
using N5.Now.Application.Handlers;
using N5.Now.Application.Interfaces;
using N5.Now.Domain.Entities;

public class CreatePermissionCommandHandlerTests
{
    [Fact]
    public async Task Create_Should_Persist_And_Index()
    {
        var repo = new Mock<IPermissionRepository>();
        var uow = new Mock<IUnitOfWork>();
        var es = new Mock<IElasticsearchService>();

        uow.SetupGet(x => x.Permissions).Returns(repo.Object);
        uow.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        repo.Setup(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Permission { Id = 1, EmployeeFirstName = "A", EmployeeLastName = "B", PermissionTypeId = 1, PermissionType = new PermissionType { Id = 1, Description = "Read" }, PermissionDate = DateTime.UtcNow });

        var handler = new CreatePermissionCommandHandler(uow.Object, es.Object);
        var result = await handler.Handle(new CreatePermissionCommand("A", "B", 1, DateTime.UtcNow), default);

        Assert.Equal(1, result.Id);
        es.Verify(x => x.IndexAsync("permissions", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
