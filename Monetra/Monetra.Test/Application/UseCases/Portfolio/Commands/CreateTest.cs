using Monetra.Application.DTOs.Portfolio;
using Monetra.Application.UseCases.Portfolio.Commands.Handler;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Portfolio;

public class CreateTest
{
    private readonly CreatePortfolioHandler _createPortfolioHandler = new(new FakeUnitOfWork());
    [Fact]
    public async Task Should_Return_False_If_Customer_No_Exists()
    {
        var request = new CreatePortfolioRequest(new CreatePortfolioDTO()
        {
            Reservation = 10,
            Title = "teste",
            Userid = Guid.NewGuid().ToString(),
            ValueFixed = 10,
            ValueVisible = 0
        });
        var result = await _createPortfolioHandler.Handle(request,CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
    [Fact]
    public async Task Should_Return_True_If_Parameters_Is_Valid()
    {
        var request = new CreatePortfolioRequest(new CreatePortfolioDTO()
        {
            Reservation = 10,
            Title = "teste",
            Userid = "3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92",
            ValueFixed = 10,
            ValueVisible = 0
        });
        var result = await _createPortfolioHandler.Handle(request,CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
}