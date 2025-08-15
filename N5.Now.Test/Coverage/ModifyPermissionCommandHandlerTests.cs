using Moq;
using N5.Now.Application.Commands;
using N5.Now.Application.Handlers;
using N5.Now.Application.Interfaces;
using N5.Now.Domain.Entities;

public class ModifyPermissionCommandHandlerTests
{
    [Fact]
    public async Task Modify_Should_Update_And_Index()
    {
        var repo = new Mock<IPermissionRepository>();
        var uow = new Mock<IUnitOfWork>();
        var es = new Mock<IElasticsearchService>();

        var existing = new Permission { Id = 2, EmployeeFirstName = "X", EmployeeLastName = "Y", PermissionTypeId = 1, PermissionDate = DateTime.UtcNow };

        uow.SetupGet(x => x.Permissions).Returns(repo.Object);
        repo.Setup(x => x.GetByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        uow.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        repo.Setup(x => x.GetByIdAsync(2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Permission { Id = 2, EmployeeFirstName = "N", EmployeeLastName = "M", PermissionTypeId = 2, PermissionType = new PermissionType { Id = 2, Description = "Write" }, PermissionDate = DateTime.UtcNow });

        var handler = new ModifyPermissionCommandHandler(uow.Object, es.Object);
        var result = await handler.Handle(new ModifyPermissionCommand(2, "N", "M", 2, DateTime.UtcNow), default);

        Assert.Equal(2, result.Id);
        es.Verify(x => x.IndexAsync("permissions", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
