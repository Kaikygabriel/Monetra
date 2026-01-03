using Monetra.Application.DTOs.Mark;
using Monetra.Application.UseCases.Mark.Command.Handler;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Test.Mock;

namespace Monetra.Test.Application.UseCases.Mark.Command;

public class CreateTeste
{
    private readonly CreateMarkHandler _handler = new CreateMarkHandler(new FakeUnitOfWork());

    [Fact]
    public async Task Should_Return_False_If_MarkCreateIsInvalid()
    {
        var markCreateDtoInvalid = new CreateMarkDTO()
        {
            CustomerId = Guid.Empty,
            DeadLine = DateTime.Now,
            Title = string.Empty,
            Value = -10
        };
        var request = new CreateMarkRequest(markCreateDtoInvalid);
        var result = await  _handler.Handle(request, new CancellationToken());
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_False_If_Customer_Of_MarkIsNull()
    {
        var markCreateDtoInvalid = new CreateMarkDTO()
        {
            CustomerId = Guid.NewGuid(),
            DeadLine = DateTime.Now.AddDays(10),
            Title = "teste",
            Value = 100
        };
        var request = new CreateMarkRequest(markCreateDtoInvalid);
        var result = await  _handler.Handle(request, new CancellationToken());
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task Should_Return_True_RequetIsValid()
    {
        var markCreateDtoInvalid = new CreateMarkDTO()
        {
            CustomerId = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92"),
            DeadLine = DateTime.Now.AddDays(20),
            Title = "teste teste teste",
            Value = 1000
        };
        var request = new CreateMarkRequest(markCreateDtoInvalid);
        var result = await  _handler.Handle(request, new CancellationToken());
        Assert.True(result.IsSuccess);
    }
}