using Moq;
using N5.Now.Application.Handlers;
using N5.Now.Application.Interfaces;
using N5.Now.Application.Queries;
using N5.Now.Domain.Entities;

public class GetPermissionQueryHandlerTests
{
    [Fact]
    public async Task Get_Should_Return_List_And_Index()
    {
        var repo = new Mock<IPermissionRepository>();
        var uow = new Mock<IUnitOfWork>();
        var es = new Mock<IElasticsearchService>();

        uow.SetupGet(x => x.Permissions).Returns(repo.Object);
        repo.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Permission> {
                new Permission { Id=3, EmployeeFirstName="A", EmployeeLastName="B", PermissionTypeId=1, PermissionType=new PermissionType{Id=1, Description="Read"}, PermissionDate=DateTime.UtcNow }
            });

        var handler = new GetPermissionQueryHandler(uow.Object, es.Object);
        var result = await handler.Handle(new GetPermissionQuery(null), default);

        Assert.Single(result);
        es.Verify(x => x.IndexAsync("permissions", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}
