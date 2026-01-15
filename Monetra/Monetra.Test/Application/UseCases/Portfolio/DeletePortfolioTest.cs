using Monetra.Application.UseCases.Portfolio.Commands.Handler;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Portfolio;

public class DeletePortfolioTest
{
    private readonly DeletePortfolioHandler _handler =new(new FakeUnitOfWork());
    [Fact]
    public async Task Should_Return_False_if_Portfolio_Not_Found()
    {
        var request = new DeletePortfolioRequest(Guid.NewGuid(), Guid.NewGuid());
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_True_If_Remove_Portfolio()
    {
        var idValid = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92");
        var request = new DeletePortfolioRequest(idValid, idValid);
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
}