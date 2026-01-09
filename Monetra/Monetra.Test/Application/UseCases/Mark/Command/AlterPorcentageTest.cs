using Monetra.Application.UseCases.Mark.Command.Handler;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Mark.Command;

public class AlterPorcentageTest
{
    private readonly AlterPercentageHandler _handler = new (new FakeUnitOfWork(),new FakeServiceEmail());

    [Fact]
    public async Task Should_Return_True_If_Parameters_Is_Valid()
    {
        var request =
            new AlterPercentageOfMarkRequest
                (Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92"), 10);
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_False_If_Customer_No_Exists()
    {
        var request =
            new AlterPercentageOfMarkRequest
                (Guid.NewGuid(), 10);
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
}