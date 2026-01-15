using Monetra.Application.UseCases.Portfolio.Commands.Handler.Transaction;
using Monetra.Application.UseCases.Portfolio.Commands.Request.Transaction;
using Monetra.Domain.BackOffice.Enum;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Portfolio.Commands.Transaction;

public class RemoveValue
{
 
    private readonly Guid PORTFOLIO_ID = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92");
    private readonly Guid CUSTOMER_ID = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92");
    private readonly RemoveValuePortfolioHandler _handler = new(new FakeUnitOfWork(), new FakeMediator());
    
    
    [Fact]
    public async Task Should_Return_False_Portfolio_NotFound()
    {
        var request =
            new RemoveValuePortfolioRequest(Guid.NewGuid(), CUSTOMER_ID, TransactionType.FixedIncome, 11, "teste");
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_FAlse_If_Customer_NotFound()
    {
        var request =
            new RemoveValuePortfolioRequest(PORTFOLIO_ID, Guid.NewGuid(), TransactionType.FixedIncome, 11, "teste");
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_True_IF_Parameters_IsVAlid()
    {
        var request =
            new RemoveValuePortfolioRequest(PORTFOLIO_ID, CUSTOMER_ID, TransactionType.FixedIncome, 0, "teste");
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
   
}