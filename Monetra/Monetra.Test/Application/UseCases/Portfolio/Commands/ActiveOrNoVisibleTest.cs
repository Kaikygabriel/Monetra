using Monetra.Application.UseCases.Portfolio.Commands.Handler;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Portfolio.Commands;

public class ActiveOrNoVisibleTest
{
    private readonly ActiveOrNoVisiblePortfolioHandler _handler = new(new FakeUnitOfWork());
    [Fact]
    public async Task Should_REturn_true_If_Portfolio_Active_Visible()
    {
        var request = new ActiveOrNoVisiblePortfolioRequest(Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92"),
            Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92"));
        var result =await _handler.Handle(request, CancellationToken.None);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Should_Return_False_if_POrtfolio_And_Customer_NOExisting()
    {
        
        var request = new ActiveOrNoVisiblePortfolioRequest(System.Guid.NewGuid(),System.Guid.NewGuid());
        var result =await _handler.Handle(request, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}